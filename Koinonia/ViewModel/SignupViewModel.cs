
using Koinonia.Models;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Koinonia.Views;
using Xamarin.Essentials;

/// <summary>
/// First page with user interaction, a Form to enter profile information. This page will only be viewed once,
/// as App.cs will check for existance of a Profile and skip this page alltogether. In this vein, user
/// cannot skip this page without entering valid information.
/// </summary>
namespace Koinonia.ViewModel
{
    public class SignupViewModel : BaseViewModel
    {
        public Profile Profile { get; set; }
        private readonly IPageService _pageService; 
        public ICommand NextButtonCommand { get; private set; }


        public SignupViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Profile = new Profile()
            {
                ContactID = 0,
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Email = ""
            };
            NextButtonCommand = new Command(Next);
        }   
             

        private async void Next()
        {
            if ((Profile.FirstName.Length > 0) && (Profile.LastName.Length > 0) &&
                (Profile.PhoneNumber.Length > 0) && (Profile.Email.Length > 0))
            {
                await _pageService.DisplayAlert("Check", "Are these details correct?", "No", "Yep!");

                //Bad practice, could fail to save profile?
                await App.Database.SaveProfileAsync(Profile);

                //Sets flag to skip this page in future.
                Preferences.Set("ProfileExists", true);
                await _pageService.PushAsync(new ContactList());
            }
            else
            {
                //Gotta have fun with people I guess
                await _pageService.DisplayAlert("Error", "Please enter all fields", "My Bad!");
            }    

            
        }

        
    }

    
}
