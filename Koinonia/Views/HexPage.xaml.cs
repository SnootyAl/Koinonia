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

        /*public async void GetScreenDimensions()
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            //screenWidth = (int)displayInfo.Width;
            //screenHeight = (int)displayInfo.Height;
            //Console.WriteLine("Screen Width: " + screenWidth);
            //await DisplayAlert("Dimensions", screenWidth + "x" + screenHeight, "Coolio");

        }*/

        /*public void CreateAndShowGrid()
        {
            var tempHexGrid = new HexLayout
            {
                //RowCount = hexRows,
                //ColumnCount = hexColumns,
                Orientation = StackOrientation.Vertical
            };

            //for(int i=0; i<hexRows; i++)
            {
                //for(int j=0; j<hexColumns; j++)
                {
                    var tempButton = new Button
                    {
                        BackgroundColor = Color.FromRgb(255, 255, 255),
                        HeightRequest = 150,
                        WidthRequest = 150,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        CornerRadius = 75,

                    };

                    //HexLayout.SetRow(tempButton, i);
                    //HexLayout.SetColumn(tempButton, j);
                    tempHexGrid.Children.Add(tempButton);

                }
            }

            ContainerGrid.Children.Add(tempHexGrid);

       }*/

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