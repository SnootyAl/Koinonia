

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
            Tnc.Source = "https://www.termsandconditionsgenerator.com/live.php?token=6QBYD6I2Q8Vs7a8D7KdXiRtVjMxkAzxW";
            PnP.Source = "https://www.freeprivacypolicy.com/privacy/view/53f05bfebffd86f34f74d081923951bc";
        }

        protected override bool OnBackButtonPressed()
        {
            var settingsViewModel = (SettingsViewModel)BindingContext;
            if (settingsViewModel.ShowTnC)
            {
                settingsViewModel.CloseTnC();
                return true;
            }
            if (settingsViewModel.ShowPrivacyPolicy)
            {
                settingsViewModel.ClosePrivacyPolicy();
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}