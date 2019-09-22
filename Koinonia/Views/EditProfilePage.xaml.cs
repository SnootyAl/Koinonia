using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.ViewModel;
using Koinonia.Models;


namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        
        public EditProfilePage(Profile mainProfile)
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(new PageService(), mainProfile);
               
        }    
    }
}