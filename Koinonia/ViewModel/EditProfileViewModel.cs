using Koinonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    class EditProfileViewModel
    {
        public Profile _mainProfile { get; set; }
        private Profile editedProfile { get; set; }
        private readonly IPageService _pageService;        
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public EditProfileViewModel(IPageService pageService, Profile mainProfile)
        {
            _pageService = pageService;
            _mainProfile = mainProfile;
            editedProfile = new Profile
            {
                FirstName = _mainProfile.FirstName,
                LastName = _mainProfile.LastName,
                Email = _mainProfile.Email,
                PhoneNumber = _mainProfile.PhoneNumber
            };
            //CancelCommand = new Command(Cancel);
            SaveCommand = new Command(Save);
            //GetProfileDetails();
        }

        /*private async void GetProfileDetails()
        {
            newProfile = await App.Database.GetProfileAsync(0);
        }*/

        /*private async void Cancel()
        {
            await _pageService.PopAsync();
        }*/

        private async void Save()
        {

            
            if (await _pageService.DisplayAlert("Confirm", "Save changes?", "Cancel", "OK"))
            {
                //As Josh mentioned, two sources of truth. Works for now, but scalability is an issue
                //Profile exists both as an Object in ProfileViewModel and a database entry, set independently.
                _mainProfile = editedProfile;
                await App.Database.UpdateProfileAsync(_mainProfile);                
                await _pageService.PopAsync();
            }
        }
    }
}
