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

        public string errorLabel = "Hello";

        public NewContactViewModel(IPageService pageService)
        {
            _pageService = pageService;
            newContact = new Contact()
            {
                FirstName = "If you can see this",
                LastName = "DataBinding is working",
                PhoneNumber = "12345"
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
            await _pageService.DisplayAlert(newContact.FirstName, newContact.LastName, "OK", "Cancel");
            Console.WriteLine("SavePressed");
            /*if ((newContact.FirstName != null) && (newContact.PhoneNumber != null))
            { 
                *//*Console.WriteLine(newContact.FirstName);
                await App.Database.SaveContactAsync(newContact);
                await _pageService.PopAsync();*//*
            }
            else
            {
                //Needs INotifyPropertyChanged
                errorLabel = "Please enter all fields";
            }*/
        }
    }
}
