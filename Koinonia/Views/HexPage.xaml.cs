using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.HexLayouts;
using Koinonia;
using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HexPage : ContentPage
    {

        readonly HexViewModel vm;
        public HexPage()
        {            
            

            InitializeComponent();
            BindingContext = vm = new HexViewModel(new PageService());
            //GetScreenDimensions();
            
        }
      

        protected override void OnAppearing()
        {
            vm.OnAppearing();
            base.OnAppearing();
        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            await DisplayAlert("Congrats", "Youre kinda there", "Coolio");
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {

        }
    }
}