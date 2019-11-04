
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : CarouselPage
    {
        

        public WelcomePage()
        {
            InitializeComponent();
            BindingContext = new WelcomeViewModel(new PageService());
        }

    }
}