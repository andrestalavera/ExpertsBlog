using ExpertsBlog.Entities;
using System.Collections.ObjectModel;

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
                    ImageUrl = "https://picsum.photos/5/5"
                });
            }
        }
    }
}