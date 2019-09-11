using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.ViewModel;


namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(new PageService());
               
        }    
    }
}