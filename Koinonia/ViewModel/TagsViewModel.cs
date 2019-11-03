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

        
        // Save the new tag into the database and added it to the observable collection 

        public Command CreateTags {
            get {
                return new Command(async () =>
                {
                    if (Tagname.TagNames.Length > 0)
                    {
                        // display alert with tag name and conformation message
                        bool answer = await _pageService.DisplayAlert(Tagname.TagNames, "This tag will be created","Cancel", "Okay");
                        
                        // if the answer is true then create and add the tag
                        if(answer == true)
                        {
                            await App.Database.SavetagAsync(Tagname);
                            TagNameCollection.Add(Tagname);
                        }                          
                    }
                    else
                    {
                        await _pageService.DisplayAlert("Error", "Tag Name was not entered", "try again");
                    }

                });
            }
        }

       // create obserable collection for the tags
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

        // get the tags from the database
        public async void showtags()
        {
            TagNameCollection = new ObservableCollection<Tags>(await App.Database.getTagsAsync());
        }

    }
}
