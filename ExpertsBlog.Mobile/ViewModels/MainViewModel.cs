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
using ExpertsBlog.Mobile.Services;

namespace ExpertsBlog.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IExpertsBlogApiService apiService = new ExpertsBlogApiService();

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
            BlogPosts = new ObservableCollection<BlogPost>(await apiService.GetBlogPosts());
        }

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
        });
    }
}