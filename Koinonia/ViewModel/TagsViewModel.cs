using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using Koinonia.Views;

namespace Koinonia.ViewModel
{
    class TagsViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        public List<string> ListData { get; set; }
        public TagsViewModel(PageService pageService)
        {
            _pageService = pageService;

            ListData = new List<string>()
            {
                "String A",
                "String B",
                "String C",
                "String D"
            };


        }

       
        // Navigation for the tag creation page using create tag button page

        public Command ChangePage {
            get {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CreateTagsPage());
                });
            }
        }

    }
}
