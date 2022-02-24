using System;
using System.Collections.Generic;
using ExpertsBlog.Mobile.ViewModels;
using Xamarin.Forms;

namespace ExpertsBlog.Mobile.Pages
{
    public partial class GeolocationPage : ContentPage
    {
        public GeolocationPage()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            ((GeolocationViewModel)BindingContext).CheckAndRequestPermissions();
        }
    }
}
