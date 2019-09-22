
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {

        
        public SignupPage()
        {
            InitializeComponent();
            BindingContext = new SignupViewModel(new PageService());
        }
        
    }
}