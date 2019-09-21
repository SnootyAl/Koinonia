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
            //contactList.ItemsSource = await App.ContactDatabase.GetContactsAsync();
        }


        //override Android back button to avoid returning to signup screen.
        // **** Explore XAMARIN for possibility of setting this screen to the new 'home' or 'base' screen? Bottom of stack? - A
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void Options_Button_Pressed(object sender, EventArgs e)
        {

            var response = await DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "New Contact", "Clear", "Debug", "Tags" );

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

                case "New Contact":

                    //****Bug? Table ID continues to increment? Probably not an issue, saves conflicts in future.
                    
                    await Navigation.PushAsync(new NewContactPage());
                    break;

                case "Clear":
                    
                    //await App.ContactDatabase.DeleteAllAsync();
                    OnAppearing();
                    break;

                case "Debug":

                    await Navigation.PushAsync(new DebugPage());
                    break;         


                case "Tags":
                    await Navigation.PushAsync(new Tags());
                    break;


            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_contacts = await App.ContactDatabase.GetContactsAsync();
            if (String.IsNullOrWhiteSpace(e.NewTextValue))
            {
                contactList.ItemsSource = _contacts;
            }
            else
            {
                contactList.ItemsSource = _contacts.Where(c => c.FirstName.Contains(e.NewTextValue));
            }
           
        }
    }
}