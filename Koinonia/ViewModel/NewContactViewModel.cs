using Koinonia.Views;
using Koinonia.Models;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using MvvmHelpers;

namespace Koinonia.ViewModel
{
    class NewContactViewModel : BaseViewModel
    {
        public Contact newContact { get; set; }
        private SQLiteAsyncConnection _connection;
        private readonly IPageService _pageService;
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ContactViewModel _parent;

        public string errorLabel = "Hello";

        public NewContactViewModel(IPageService pageService, ContactViewModel parent)
        {
            _pageService = pageService;
            _parent = parent;
            newContact = new Contact
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = ""
            };
            CancelCommand = new Command(CancelPressed);
            SaveCommand = new Command(SavePressed);
        }

        private async void CancelPressed()
        {
            await _pageService.PopAsync();
        }

        private async void SavePressed()
        {
            
            Console.WriteLine("SavePressed");
            if ((newContact.FirstName.Length > 0) && (newContact.LastName.Length > 0) &&
                (newContact.PhoneNumber.Length > 0))
            {
                await _pageService.DisplayAlert(newContact.FirstName, newContact.LastName, "Cancel", "OK");
                await App.Database.SaveContactAsync(newContact);
                Console.WriteLine(newContact);
                _parent.AddContact(newContact);
                await _pageService.PopAsync();
            }
            else
            {
                await _pageService.DisplayAlert("Error", "Please enter all fields", "My Bad!");
            }
        }
    }
}
