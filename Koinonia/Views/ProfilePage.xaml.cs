
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(new PageService());

        }

        
    }
}