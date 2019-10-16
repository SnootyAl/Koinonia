using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
/// @Roshens page
/// </summary>
namespace Koinonia.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public bool _showTnC = false;
        public bool _showPrivacyPolicy = false;
        public SettingsViewModel()
        {
            Title = "Settings";
            OpenTnC = new Command(() => { ShowTnC = true; });
            OpenPrivacyPolicy = new Command(() => { ShowPrivacyPolicy = true; });
        }

        public bool ShowTnC
        {
            get => _showTnC;
            set
            {
                _showTnC = value;
                OnPropertyChanged("ShowTnC");
            }
        }

        public bool ShowPrivacyPolicy
        {
            get => _showPrivacyPolicy;
            set
            {
                _showPrivacyPolicy = value;
                OnPropertyChanged("ShowPrivacyPolicy");
            }
        }

        public ICommand OpenTnC { get; }
        public ICommand OpenPrivacyPolicy { get; }
        public void CloseTnC()
        {
            ShowTnC = false;
        }

        public void ClosePrivacyPolicy()
        {
            ShowPrivacyPolicy = false;
        }
    }
}
