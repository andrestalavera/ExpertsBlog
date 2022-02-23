using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using ExpertsBlog.Mobile.Services;
using System.Threading.Tasks;

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
            apiService = new ExpertsBlogApiService();
            BlogPosts = new ObservableCollection<BlogPost>();
        }

        public void GetData()
        {
            Task.Run(async () =>
            {
                var items = await apiService.GetBlogPosts();
                foreach (var item in items)
                {
                    BlogPosts.Add(item);
                }
            });
        }

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
        });
    }
}