﻿using System;
using Xamarin.Forms;

namespace ExpertsBlog.Mobile.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class DetailsViewModel : ViewModelBase
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                LoadItem(value);
            }
        }

        private string content;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        private DateTime creation;
        public DateTime Creation
        {
            get => creation;
            set => SetProperty(ref creation, value);
        }

        private void LoadItem(int id)
        {
            Id = id;
            Content = "New content";
            Creation = DateTime.Today;
        }
    }
}