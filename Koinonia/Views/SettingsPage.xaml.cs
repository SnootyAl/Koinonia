

using Koinonia.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(new PageService());
        }
    }
}