using ExpertsBlog.Mobile.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ExpertsBlog.Mobile.Pages
{
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((CameraViewModel)BindingContext).Initialize();
        }
    }
}
