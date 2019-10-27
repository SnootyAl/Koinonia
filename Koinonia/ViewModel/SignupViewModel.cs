
using Koinonia.Models;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Koinonia.Views;
using Xamarin.Essentials;
using Plugin.Media;
using System;
using Plugin.Media.Abstractions;


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
                await _pageService.PushAsync(new ContactPage());
            }
            else
            {
                //Gotta have fun with people I guess
                await _pageService.DisplayAlert("Error", "Please enter all fields", "My Bad!");
            }    

            
        }

        // Photo Support 
        // All the user to import a photo for the profile picture

        public Command addimage {
            get
            {
                return new Command(async () =>
                {
                    await CrossMedia.Current.Initialize();
                    var storagestatus = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);


                    // Check for decive compatiablity 
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await _pageService.DisplayAlert("Not Supported", "This device is not supported with this feature", "Ok");

                    }

                    // test image button press
                    // await _pageService.DisplayActionSheet("test", "testing button press commands", "okay");

                    // set the size of the image 

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Small
                    };

                    // set the selected image to that size 
                    var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    // check to see if image is not null 

                    if(selectedImageFile == null)
                    {
                        await _pageService.DisplayAlert("Error", "there was a problem with the image, please try again", "ok");
                    }
                    
                });
            }
        }


                
    }

    
}
