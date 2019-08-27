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
        private ObservableCollection<Contact> _contacts;

        //Handles search functionality. On change, calls an updated contact list with GetContacts()
        // And makes this list the new ItemsSource of contactList - A
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactList.ItemsSource = GetContacts(e.NewTextValue);
        }


        //Returns an IEnumerable of current contacts. Also handles filtering (currently only basic text matching)
        //Will likely be built upon in future to pull contacts from memory rather than hard coding, as well as additional filtering. - A
        IEnumerable<Contact> GetContacts(String searchText = null)
        {
            //Placeholder hard coded contacts. WIll be more sophisiticated once database is implemented. - A
            /*var contacts = new List<Contact>
            {
                new Contact {FirstName = "Alex", LastName = "Raymond", ImageURL = "/ContactImages/contact1" },
                new Contact { FirstName = "Jordan", ImageURL = "http://lorempixel.com/100/100/people/2",
                    Status = "Hello World" },
                new Contact {FirstName = "Calum", ImageURL = "http://placehold.it/100x100"},
                new Contact {FirstName = "Roshen", Status = "Hi, Hows it going?" }

             };*/


            //Search functionality - A
            if (String.IsNullOrWhiteSpace(searchText))
            {
                return _contacts;
            }

            return _contacts.Where(c => c.FirstName.Contains(searchText));
        }



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
            await _connection.CreateTableAsync<Contact>();
            var contacts = await _connection.Table<Contact>().ToListAsync();
            _contacts = new ObservableCollection<Contact>(contacts);
            contactList.ItemsSource = _contacts;
            
            base.OnAppearing();
        }


        //override Android back button to avoid returning to signup screen.
        // **** Explore XAMARIN for possibility of setting this screen to the new 'home' or 'base' screen? Bottom of stack? - A
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void Options_Button_Pressed(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "New Contact", "Clear" );
            switch (response)
            {

                //**BUG** Title back button persists for a moment after pressing, allowing user to spam button and return to SignupPage
                case "Profile":
                    await Navigation.PushAsync(new ProfilePage());
                    break;

                case "Settings":
                    await Navigation.PushAsync(new SettingsPage());
                    break;

                case "New Contact":
                    var newContact = new Contact { FirstName = "Added" + DateTime.Now.Ticks };
                    await _connection.InsertAsync(newContact);
                    _contacts.Add(newContact);
                    //await Navigation.PushAsync(new NewContactPage());
                    break;

                case "Clear":
                    _contacts.Clear();
                    await _connection.DeleteAllAsync<Contact>();
                    break;

            }
        }
    }
}