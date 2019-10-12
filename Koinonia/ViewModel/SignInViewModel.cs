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
    public class SignInViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        public Profile email { get; set; }

        public ICommand testCommand { get; private set; }

        public SignInViewModel(PageService pageService)
        {
            _pageService = pageService;

            email = new Profile
            {
                Email = ""
            };

            testCommand = new Command(SigninPressed);
        }

        private async void SigninPressed()
        {
            Console.WriteLine(email.Email);
            await _pageService.DisplayAlert("Working", "okay", "Cancel");
        }

    }
}
