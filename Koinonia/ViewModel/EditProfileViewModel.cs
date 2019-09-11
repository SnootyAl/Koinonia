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
        public Profile newProfile { get; set; }
        private readonly IPageService _pageService;        
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public EditProfileViewModel(IPageService pageService)
        {
            _pageService = pageService;
            CancelCommand = new Command(Cancel);
            SaveCommand = new Command(Save);
            newProfile = new Profile()
            {
                FirstName = "TempContact",
                LastName = "HardCoded",
                PhoneNumber = "12345",
                Email = "Helloworld@email.com"                
            };
        }

        private async void Cancel()
        {
            await _pageService.PopAsync();
        }

        private async void Save()
        {

            //Implement a check for all fields valid
            if (await _pageService.DisplayAlert("Confirm", "Save changes?", "Save", "Cancel"))
            {
                //Implement Database saving                
                await _pageService.PopAsync();
            }
        }
    }
}
