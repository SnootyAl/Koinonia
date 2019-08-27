using Koinonia.Models;
using System;
using System.Collections.Generic;
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactList.ItemsSource = GetContacts(e.NewTextValue);
        }

        IEnumerable<Contact> GetContacts(String searchText = null)
        {
            var contacts = new List<Contact>
            {
                new Contact {firstName = "Alex", lastName = "Raymond", ImageURL = "/ContactImages/contact1" },
                new Contact { firstName = "Jordan", ImageURL = "http://lorempixel.com/100/100/people/2",
                    Status = "Hello World" },
                new Contact {firstName = "Calum", ImageURL = "http://placehold.it/100x100"},
                new Contact {firstName = "Roshen", Status = "Hi, Hows it going?" }

             };

            if (String.IsNullOrWhiteSpace(searchText))
            {
                return contacts;
            }

            return contacts.Where(c => c.firstName.Contains(searchText));
        }


        public ContactList()
        {
            InitializeComponent();

            contactList.ItemsSource = GetContacts();
        }

        
    }
}