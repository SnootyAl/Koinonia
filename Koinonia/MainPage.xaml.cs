using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Koinonia
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int screenWidth, screenHeight;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
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
