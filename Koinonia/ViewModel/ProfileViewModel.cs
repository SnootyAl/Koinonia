using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    class ProfileViewModel : BaseViewModel
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        private readonly IPageService _pageService;
        public Profile debugProfile { get; set; }

        public ProfileViewModel(IPageService pageService)
        {
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            _pageService = pageService;
            debugProfile = new Profile()
            {
                FirstName = "Testing",
                LastName = "Debug",
                PhoneNumber = "12345",
                Email = "notarealemail@email.com"
            };

            debugProfile.OnPropertyChanged(nameof(ProfileViewModel));

        }


        private async void Delete()
        {
            try
            {
                var profileTemp = await App.Database.GetProfileAsync(0);
                if (await _pageService.DisplayAlert("Confirmation", "Are you sure you wish to delete Profile?", "Confirm", "Cancel"))
                {
                    await App.Database.DeleteProfileAsync();
                    //Need to figure out preferences
                    //Preferences.Set("ProfileExists", false);
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
