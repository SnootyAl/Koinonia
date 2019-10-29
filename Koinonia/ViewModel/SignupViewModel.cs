
using Koinonia.Models;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Koinonia.Views;
using Xamarin.Essentials;
using Plugin.Media;
using System;
using Plugin.Media.Abstractions;
using System.Text.RegularExpressions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;



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
        public ICommand Photobutton { get; private set; }

        // image for profile picture 
        private string image { get; set; }


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
            Photobutton = new Command(photopermission);

        }


        private async void Next()
        {
            // check if email is valid 
            var regexemail = new Regex("[@]");

            if ((Profile.FirstName.Length > 0) && (Profile.LastName.Length > 0) &&
                (Profile.PhoneNumber.Length > 0) && (Profile.Email.Length > 0) && regexemail.IsMatch(Profile.Email))
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

        // ask user for permission to access the photo storage. 

        private async void photopermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            // if the current status is false then ask for permission
              
            if(status != PermissionStatus.Granted)
            {
              if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
              {
                 await _pageService.DisplayAlert("Need photo", "Please allow access for photo", "Allow");
              }
              var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
              
              if (results.ContainsKey(Permission.Storage))
              {
                  status = results[Permission.Storage];
              }
            }
              if(status == PermissionStatus.Granted)
              {
                 photos();
              }
  
        }


        // Photo Support 
        // All the user to import a photo for the profile picture

        private async void photos()
        {
            await CrossMedia.Current.Initialize();

            var storagestatus = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);

            // create string for image 


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

            if (selectedImageFile == null)
            {
                await _pageService.DisplayAlert("Error", "there was a problem with the image, please try again", "ok");
                image = "add_photo_default.png";

            }

            Console.WriteLine("helllllo");
        }

        


    }
}
