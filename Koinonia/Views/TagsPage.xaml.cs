

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Koinonia.ViewModel;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagsPage : ContentPage
    {
        public TagsPage()
        {
            InitializeComponent();
            BindingContext = new TagsViewModel(new PageService());
        }
    }
}