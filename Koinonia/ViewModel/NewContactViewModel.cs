using Koinonia.Views;
using Koinonia.Models;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using MvvmHelpers;

/// <summary>
/// Form to add a new contact with the default required fields. Adds newly created contact to the Contact database.
/// </summary>
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

        public NewContactViewModel(IPageService pageService, ContactViewModel parent)
        {
            _pageService = pageService;
            _parent = parent;
            newContact = new Contact
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Notes = ""
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
            
           /*Kinda poorly implemented check for all mandatory fields present. At the moment we have no real
           minimum requirements for a contact, this could change in the future and these checks would change accordingly*/
            if ((newContact.FirstName.Length > 0) && (newContact.LastName.Length > 0) &&
                (newContact.PhoneNumber.Length > 0))
            {
                await App.Database.SaveContactAsync(newContact);
                Console.WriteLine(newContact);
                _parent.AddContact(newContact);
                await _pageService.PopAsync();
            }
            else
            {
                //Gotta have fun with people I guess
                await _pageService.DisplayAlert("Error", "Please enter all fields", "My Bad!");
            }
        }
    }
}
