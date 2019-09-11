using Koinonia.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        private Profile tempProfile;
        public EditProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            tempProfile = await App.Database.GetProfileAsync(0);
            profileDetails.BindingContext = tempProfile;
        }

        private async void CancelButton_Pressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_Pressed(object sender, EventArgs e)
        {

            //is this poor practice? - Alex
            if(await DisplayAlert("Confirm", "Save changes?", "Save", "Cancel"))
            {
                await App.Database.SaveProfileAsync(tempProfile);
                await Navigation.PopAsync();
            }
        }
    }
}