using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

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
            for (int i = 0; i < 10; i++)
            {
                BlogPosts.Add(new BlogPost
                {
                    Title = "Title " + i,
                    ImageUrl = "https://picsum.photos/10/10",
                    Author = "Author " + i
                });
            }
        }

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
        });
    }
}