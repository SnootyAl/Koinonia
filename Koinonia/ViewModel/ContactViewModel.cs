using Koinonia.Models;
using Koinonia.Views;
using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;


namespace Koinonia.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        public ICommand TempButtonCommand { get; private set; }
        public ICommand SearchTextChangedCommand { get; private set; }
        private readonly IPageService _pageService;
        private SQLiteAsyncConnection _connection;

        private ObservableCollection<Contact> _contacts { get; set; }

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value)
                {
                    return;
                }
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }

        }

        


        public ContactViewModel(IPageService pageService)
        {
            TempButtonCommand = new Command(TempButtonPressed);
            SearchTextChangedCommand = new Command(SearchBarTextChanged);
            _pageService = pageService;
            SetContactCollection();
            //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            
            
        }

        public async void SetContactCollection()
        {            
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());            
        }
        
       


        async void TempButtonPressed()
        {

            var response = await _pageService.DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "New Contact", "Clear", "Debug", "Tags");

            switch (response)
            {

                //**BUG** Title back button persists for a moment after pressing, allowing user to spam button and return to SignupPage
                case "Profile":
                    await _pageService.PushAsync(new ProfilePage());
                    break;

                case "Settings":
                    await _pageService.PushAsync(new SettingsPage());
                    break;

                //Temporary add new contact for list debugging and search. Will delete once proper add contact implemented.
                /*case "New Contact":
                    var newContact = new Contact { FirstName = "Added" + DateTime.Now.Ticks };
                    await _connection.InsertAsync(newContact);
                    _contacts.Add(newContact);
                    //await Navigation.PushAsync(new NewContactPage());
                    break;*/

                case "New Contact":

                    //****Bug? Table ID continues to increment? Probably not an issue, saves conflicts in future.

                    await _pageService.PushAsync((new NewContactPage(this)));
                    SetContactCollection();
                    break;

                case "Clear":

                    await App.Database.DeleteAllContactsAsync();                    
                    break;

                case "Debug":

                    //await Navigation.PushAsync(new DebugPage());
                    break;


                case "Tags":
                    await _pageService.PushAsync(new Tags());
                    break;


            }
        }

        private async void SearchBarTextChanged()
        {
            //Doesnt convert well from old implementation

        }
    }

 
}
