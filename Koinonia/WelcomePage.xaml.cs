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
    public partial class WelcomePage : ContentPage
    {
        public int screenWidth, screenHeight;

        public WelcomePage()
        {
            InitializeComponent();
        }

        public int buttonSize = 10;

        async void Button_Pressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var displayInfo = DeviceDisplay.MainDisplayInfo;
            screenWidth = (int)displayInfo.Width;
            screenHeight = (int)displayInfo.Height;

        }
    }
}