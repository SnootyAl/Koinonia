using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;

namespace Koinonia.ViewModel
{
    class TagsViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;
        public TagsViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        string name = "hello world";

        public string Name {
            get => name;
            set {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
