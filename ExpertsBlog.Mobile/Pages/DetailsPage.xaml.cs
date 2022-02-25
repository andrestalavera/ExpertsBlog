using ExpertsBlog.Mobile.ViewModels;
using Xamarin.Forms;

namespace ExpertsBlog.Mobile.Pages
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<DetailsViewModel, string>(this, "Distance", async (sender, km) =>
            {
                await DisplayAlert("Kilomètres calculés", km, "OK");
            });
        }
    }
}
