using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewContactPage : ContentPage
    {
        public NewContactPage(ContactViewModel parent)
        {
            InitializeComponent();
            BindingContext = new NewContactViewModel(new PageService(), parent);
        }
    }
}