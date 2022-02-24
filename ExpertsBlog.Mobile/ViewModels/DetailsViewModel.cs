using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Services;
using Xamarin.Essentials;
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
        private readonly IExpertsBlogApiService apiService;

        #region Properties
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

        private ObservableCollection<Address> addresses;
        public ObservableCollection<Address> Addresses
        {
            get => addresses;
            set => SetProperty(ref addresses, value);
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
        #endregion

        public DetailsViewModel()
        {
            apiService = DependencyService.Get<IExpertsBlogApiService>();
            Addresses = new ObservableCollection<Address>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id de l'élément à charger</param>
        private void LoadItem(int id)
        {
            Task.Run(async () =>
            {
                var blogPost = await apiService.GetBlogPost(id);
                Author = blogPost.Author;
                Creation = blogPost.Creation;
                Category = blogPost.Category;
                Content = blogPost.Content;
                Title = blogPost.Title;

                foreach (var address in await apiService.GetAddresses(id))
                {
                    Addresses.Add(address);
                }
            });
        }

        public ICommand ClickCommand => new Command(() => Index++);

        public ICommand OpenMapsCommand => new Command<Address>(async address => 
        {
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/map/native-map-app
            string a = $"{address.Street}, {address.Zip} {address.City}, France";
            if (Device.RuntimePlatform == Device.iOS)
            {
                // https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                await Launcher.OpenAsync("http://maps.apple.com/?q=" + a);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // open the maps app directly
                await Launcher.OpenAsync("geo:0,0?q=" + a);
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                await Launcher.OpenAsync("bingmaps:?where=" + a);
            }
        });
    }
}