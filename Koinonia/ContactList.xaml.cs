using Koinonia.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactList : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private List<Contact> _contacts;

        //Handles search functionality. On change, calls an updated contact list with GetContacts()
        // And makes this list the new ItemsSource of contactList - A
        /*private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactList.ItemsSource = GetContacts(e.NewTextValue);
        }*/

        //Builder function, calls GetContacts(). - A
        public ContactList()
        {
            InitializeComponent();

            Console.WriteLine("InitComponent");

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            Console.WriteLine("_connection completed");

            //contactList.ItemsSource = GetContacts();
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            contactList.ItemsSource = await App.Database.GetContactsAsync();
        }


        //override Android back button to avoid returning to signup screen.
        // **** Explore XAMARIN for possibility of setting this screen to the new 'home' or 'base' screen? Bottom of stack? - A
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void Options_Button_Pressed(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "New Contact", "Clear");
            switch (response)
            {

                //**BUG** Title back button persists for a moment after pressing, allowing user to spam button and return to SignupPage
                case "Profile":
                    await Navigation.PushAsync(new ProfilePage());
                    break;

                case "Settings":
                    await Navigation.PushAsync(new SettingsPage());
                    break;

                    //Temporary add new contact for list debugging and search. Will delete once proper add contact implemented.
                    /*case "New Contact":
                        var newContact = new Contact { FirstName = "Added" + DateTime.Now.Ticks };
                        await _connection.InsertAsync(newContact);
                        _contacts.Add(newContact);
                        //await Navigation.PushAsync(new NewContactPage());
                        break;*/


                    //https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/database?pivots=windows

                case "New Contact":
                    
                    await Navigation.PushAsync(new NewContactPage());

                    Console.WriteLine("Nope");

                    break;

                case "Clear":
                    _contacts.Clear();
                    await _connection.DeleteAllAsync<Contact>();
                    break;

            }
        }
    }
}