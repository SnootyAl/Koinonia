using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using Koinonia.Views;

namespace Koinonia.ViewModel
{
    class CreateTagsViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        public Command PrintText { get; set; }
        public CreateTagsViewModel(PageService pageService)
        {
            _pageService = pageService;
           /* PrintText = new Command();*/
        }

        string hello = "hello world";

        public string Name 
        {
            get => hello;
            set 
            {
                SetProperty(ref hello, value);
                OnPropertyChanged(nameof(Name));
            }
        }


     }
}
