using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Pages;
using System.Collections.ObjectModel;
using System;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpertsBlog.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<BlogPost> blogPosts = new ObservableCollection<BlogPost>();
        public ObservableCollection<BlogPost> BlogPosts
        {
            get => blogPosts;
            set => SetProperty(ref blogPosts, value);
        }

        public MainViewModel()
        {
            GetData();
        }

        private async void GetData()
        {
            using(HttpClient httpclient = new HttpClient()
            {
                BaseAddress = new Uri("https://expertsblogapi.azurewebsites.net/")
            })
            {
                var json = await httpclient.GetStringAsync("BlogPosts");
                Debug.WriteLine("************************************************" + json);
                var x = JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(json);
                foreach (var blogPost in x)
                {
                    BlogPosts.Add(blogPost);
                }
            }
        }

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
        });
    }
}