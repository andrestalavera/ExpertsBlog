using ExpertsBlog.Mobile.Pages;
using Xamarin.Forms;

namespace ExpertsBlog.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        }
    }
}
