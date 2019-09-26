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

            Tagname = new Tags
            {
                TagNames = ""
            };
            showtags();
        }


        // Navigation for the tag creation page using create tag button page

        public Command ChangePage {
            get {
                return new Command(async () =>
                {
                    if (Tagname.TagNames.Length > 0)
                    {
                        // display alert with tag name and 
                        await _pageService.DisplayAlert(Tagname.TagNames, "This tag will be created", "Okay");
                        await App.Database.SavetagAsync(Tagname);
                        TagNameCollection.Add(Tagname);
                        // test to see what is being printed
                        Console.WriteLine(Tagname);
                        Console.WriteLine(Tagname.TagNames);
                        
                    }
                    else
                    {
                        await _pageService.DisplayAlert("error", "Null value", "try again");
                    }

                });
            }
        }

       
        private ObservableCollection<Tags> _tags { get; set; }
        public ObservableCollection<Tags> TagNameCollection {
            get { return _tags; }
            set {
                if (_tags == value)
                {
                    return;
                }
                _tags = value;
                OnPropertyChanged(nameof(TagNameCollection));
            }
        }

        public async void showtags()
        {
            TagNameCollection = new ObservableCollection<Tags>(await App.Database.getTagsAsync());
        }

    }
}
