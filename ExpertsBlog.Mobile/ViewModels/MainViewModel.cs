using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using ExpertsBlog.Mobile.Services;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;
using System;

namespace ExpertsBlog.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IExpertsBlogApiService apiService;

        private ObservableCollection<BlogPost> blogPosts;
        public ObservableCollection<BlogPost> BlogPosts
        {
            get => blogPosts;
            set => SetProperty(ref blogPosts, value);
        }

        public MainViewModel()
        {
            apiService = DependencyService.Get<IExpertsBlogApiService>();
        }

        public void Initialize()
        {
            Task.Run(GetBlogPosts);
        }

        private async Task GetBlogPosts()
        {
            var items = await apiService.GetBlogPosts();
            BlogPosts = new ObservableCollection<BlogPost>(items);
        }

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
        });
    }
}