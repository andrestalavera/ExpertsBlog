using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using ExpertsBlog.Entities;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ExpertsBlog.Mobile.ViewModels
{
    /// <summary>
    /// La classe DetailsViewModel hérite de ViewModelBase.
    /// L'attribut QueryProperty permet de déterminer la propriété 
    /// qui nous sers à naviguer.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public class DetailsViewModel : ViewModelBase
    {
        private int id;
        /// <summary>
        /// Id du billet de blog
        /// </summary>
        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                // On charge l'élément
                LoadItem(value);
            }
        }

        private string title;
        /// <summary>
        /// Titre du billet de blog
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string content;
        /// <summary>
        /// Contenu du billet de blog
        /// </summary>
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        private Category category;
        /// <summary>
        /// Catégorie
        /// </summary>
        public Category Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        private string author;
        /// <summary>
        /// Auteur
        /// </summary>
        public string Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }

        private DateTime creation;
        /// <summary>
        /// Date de création
        /// </summary>
        public DateTime Creation
        {
            get => creation;
            set => SetProperty(ref creation, value);
        }

        private int index;
        
        /// <summary>
        /// Index pour l'exemple de la commande
        /// </summary>
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id de l'élément à charger</param>
        private async void LoadItem(int id)
        {
            using (HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://expertsblogapi.azurewebsites.net")
            })
            {
                string blogPostJson = await httpClient.GetStringAsync($"BlogPosts/{id}");
                BlogPost blogPost = JsonConvert.DeserializeObject<BlogPost>(blogPostJson);

                string categoryJson = await httpClient.GetStringAsync($"Categories/{blogPost.CategoryId}");
                Category category = JsonConvert.DeserializeObject<Category>(categoryJson);

                Author = blogPost.Author;
                Creation = blogPost.Creation;
                Content = blogPost.Content;
                Title = blogPost.Title;
                Category = category;
            }
        }

        public ICommand ClickCommand => new Command(() => Index++);
    }
}