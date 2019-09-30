using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Koinonia.Views;



/// <summary>
/// Handles displaying the profile information. Fairly barebones, handles navigation to the edit profile page.
/// </summary>
namespace Koinonia.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
       
        private readonly IPageService _pageService;        
        private Profile _profile { get; set; }
        public Profile Profile
        {
            get { return _profile; }
            set
            {
                if (_profile == value)
                {
                    return;
                }
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public ProfileViewModel(IPageService pageService)
        {
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);            
            _pageService = pageService;
            SetProfile();
        }

        private async void SetProfile()
        {
            Profile = await App.Database.GetProfileAsync(0);
        }

        
        //Delete Profile is not really intended to be a feature of the app, in its current state it exists
        //to allow the tester to return to the first two screens as these are normally skipped if a profile exists
        private async void Delete()
        {
            try
            {
                
                if (await _pageService.DisplayAlert("Confirmation", "Are you sure you wish to delete Profile?", "Cancel", "Confirm"))
                {
                    await App.Database.DeleteProfileAsync();
                    Preferences.Set("ProfileExists", false);


                    //Bugged, takes you back to Profile creation screen.
                    //Actually not a terrible idea, as profile deletion needs a new profile?
                    //"Its not a bug, its a feature!?"
                    await _pageService.PopAsync();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
        }

        private async void Edit()
        {
            await _pageService.PushAsync(new EditProfilePage(this));
        }

        public void UpdateProfile(Profile newProfile)
        {
            Profile = newProfile;
        }
    }
}
