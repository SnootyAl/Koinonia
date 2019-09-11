using Koinonia.Models;
using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace Koinonia.ViewModel
{
    class ContactViewModel : BaseViewModel
    {
        public ICommand TempButtonCommand { get; private set; }
        public ICommand SearchTextChangedCommand { get; private set; }
        private readonly IPageService _pageService;
        private SQLiteAsyncConnection _connection;
        public List<Contact> contacts;

        public ContactViewModel(IPageService pageService)
        {
            TempButtonCommand = new Command(TempButtonPressed);
            SearchTextChangedCommand = new Command(SearchBarTextChanged);
            _pageService = pageService;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
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

                    await _pageService.PushAsync(new NewContactPage());
                    break;

                case "Clear":

                    await App.Database.DeleteAllAsync();                    
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
