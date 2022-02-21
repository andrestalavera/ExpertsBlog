using System;
using System.Windows.Input;
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

        private int index;
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
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
            //Id = id;
            Content = "New content";
            Creation = DateTime.Today;
        }

        public ICommand ClickCommand => new Command(() =>
        {
            Index++;
        });
    }
}