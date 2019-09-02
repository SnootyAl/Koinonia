using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            profileDetails.BindingContext = await App.ContactDatabase.GetProfileAsync();            
        }

        private async void DeleteButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                var profileTemp = await App.ContactDatabase.GetProfileAsync();
                Console.WriteLine(profileTemp.ContactID);
                if (await DisplayAlert("Confirmation", "Are you sure you wish to delete Profile?", "Confirm", "Cancel"))
                {
                    await App.ContactDatabase.DeleteProfileAsync();
                    Preferences.Set("ProfileExists", false);
                    await Navigation.PopAsync();
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            

            OnAppearing();
        }
    }
}