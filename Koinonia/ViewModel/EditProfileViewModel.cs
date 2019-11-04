using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
/// Edit profile page. May become obsolete: Could implement something similar to ContactInfoViewModel where the fields become
/// editable without the need for navigation to a separate page.
/// </summary>
namespace Koinonia.ViewModel
{
    class EditProfileViewModel : BaseViewModel
    {
        public Profile _mainProfile { get; set; }
        public Profile editedProfile { get; set; }
        public ProfileViewModel _parent;
        private readonly IPageService _pageService;        
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public EditProfileViewModel(IPageService pageService, ProfileViewModel parent)
        {
            _parent = parent;
            _pageService = pageService;
            _mainProfile = _parent.Profile;
            editedProfile = new Profile
            {
                FirstName = _mainProfile.FirstName,
                LastName = _mainProfile.LastName,
                Email = _mainProfile.Email,
                PhoneNumber = _mainProfile.PhoneNumber,
                Position = _mainProfile.Position, 
                Location = _mainProfile.Location,
                Work = _mainProfile.Work,
                ImageURL = _mainProfile.ImageURL
            };
            
            SaveCommand = new Command(Save);
            
        }


        private async void Save()
        {
            
            if (await _pageService.DisplayAlert("Confirm", "Save changes?", "Cancel", "OK"))
            {
                //As Josh mentioned, two sources of truth. Works for now, but scalability is an issue
                //Profile exists both as an Object in ProfileViewModel and a database entry, set independently.
                _mainProfile = editedProfile;
                await App.Database.UpdateProfileAsync(_mainProfile);
                _parent.UpdateProfile(_mainProfile);
                await _pageService.PopAsync();
            }
        }
    }
}
