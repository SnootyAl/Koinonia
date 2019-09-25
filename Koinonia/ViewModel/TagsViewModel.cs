using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using Koinonia.Views;
using SQLite;
using Koinonia.Models;
using System.Collections.ObjectModel;

namespace Koinonia.ViewModel
{
    public class TagsViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        // create db connection 
        private SQLiteAsyncConnection _connection;

        public ICommand Save { get; private set; }

        public List<string> ListData { get; set; }

        public Tags Tagname { get; set; }
        public TagsViewModel(PageService pageService)
        {
            _pageService = pageService;

           /* ListData = new List<string>()
            {
                "Hello"
            };*/

            Tagname = new Tags
            {
                TagName = ""
            };
        }

        string test = "yeet";

        // Navigation for the tag creation page using create tag button page

        public Command ChangePage {
            get {
                return new Command(async () =>
                {
                    Console.WriteLine("Hello");
                    await _pageService.DisplayAlert(Tagname.TagName, "cancel ", "okay");
                    await App.Database.SavetagAsync(Tagname);
                });
            }
        }


        private ObservableCollection<Tags> _tags { get; set; }
        public ObservableCollection<Tags> tags {
            get { return _tags; }
            set {
                if (_tags == value)
                {
                    return;
                }
                _tags = value;
                OnPropertyChanged(nameof(tags));
            }
        }

    }
}
