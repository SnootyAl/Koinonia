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
    public partial class DebugPage : ContentPage
    {

        
        
        
        public DebugPage()
        {            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 0
            };

            for (int i=0; i<15; i++)
            {
                layout.Children.Add(new Label
                {
                    Text = "String"
                });
            }

            // building other elements here, ie: Grid           

            //whaddup.BindingContext = layout;

            
            base.OnAppearing();
            //ContactGrid.ItemsSource = list1;
            //ContactGrid2.ItemsSource = list2;

        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            await DisplayAlert("Congrats", "Youre kinda there", "Coolio");
        }
    }
}