using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Koinonia.Models;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public Profile tempProfile;
        public ProfilePage()
        {
            InitializeComponent();
            tempProfile = new Profile
            {
                FirstName = "Hello",
                LastName = "World",
                PhoneNumber = "12039",
                Email = "SLJDHF"
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            
        }

        private async void DeleteButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                var profileTemp = await App.Database.GetProfileAsync(0);
                if (await DisplayAlert("Confirmation", "Are you sure you wish to delete Profile?", "Confirm", "Cancel"))
                {
                    await App.Database.DeleteProfileAsync();
                    Preferences.Set("ProfileExists", false);
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }




            OnAppearing();
        }

        private async void EditButton_Pressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfilePage());
            
        }
    }
}