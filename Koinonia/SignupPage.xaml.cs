using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koinonia.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {

        private Profile profile;
        public SignupPage()
        {
            InitializeComponent();
            profile = new Profile();
            BindingContext = profile;
        }

        async void Button_Pressed(object sender, EventArgs e)
        {
            await DisplayAlert(profile.FirstName, profile.ContactID.ToString(), "Cancel");
            await App.Database.SaveProfileAsync(profile);
            Preferences.Set("ProfileExists", true);
            await Navigation.PushAsync(new ContactList());
        }
    }
}