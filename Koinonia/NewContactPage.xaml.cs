using Koinonia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewContactPage : ContentPage
    {
        private Contact contact;
        private SQLiteAsyncConnection _connection;

        public NewContactPage()
        {
            InitializeComponent();
            
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            contact = new Contact();


        }

        private async void Cancel_Pressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Save_Pressed(object sender, EventArgs e)
        {
            if ((contact.FirstName != null) && (contact.PhoneNumber != null))
            {
                /*var contact = (Contact)BindingContext;
                contact.FirstName = First.Text.ToString();
                contact.PhoneNumber = Phone.Text.ToString();*/
                Console.WriteLine(contact.FirstName);
                //await App.ContactDatabase.SaveContactAsync(contact);
                await Navigation.PopAsync();
            }
            else
            {
                ErrorLabel.IsVisible = true;
            }
        }


        //****Change to Binding please, this is ugly -A
        private void First_TextChanged(object sender, TextChangedEventArgs e)
        {
            contact.FirstName = e.NewTextValue;
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            contact.PhoneNumber = e.NewTextValue;
        }
    }
}