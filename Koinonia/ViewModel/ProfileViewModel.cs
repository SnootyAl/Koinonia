using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Koinonia.Views;

namespace Koinonia.ViewModel
{
    class ProfileViewModel : BaseViewModel
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        //public ICommand AppearingCommand { get; private set; }
        private readonly IPageService _pageService;


        //Super Jank but it works so sue me:
        Profile _debugProfile { get; set; }
        public Profile debugProfile
        {
            get { return _debugProfile; }
            set
            {
                if (_debugProfile == value)
                {
                    return;
                }
                _debugProfile = value;
                OnPropertyChanged(nameof(debugProfile));
            }
        }

        public ProfileViewModel(IPageService pageService)
        {
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            //AppearingCommand = new Command(Appearing);
            _pageService = pageService;
            SetProfile();
        }

        private async void SetProfile()
        {
            debugProfile = await App.Database.GetProfileAsync(0);
        }


        private async void Delete()
        {
            try
            {
                
                if (await _pageService.DisplayAlert("Confirmation", "Are you sure you wish to delete Profile?", "Confirm", "Cancel"))
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
            await _pageService.PushAsync(new EditProfilePage());
        }
    }
}
