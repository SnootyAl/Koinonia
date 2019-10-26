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
        public bool _showTnCButton = true;
        public bool _showPrivacyPolicyButton = true;
        public bool _showPrivacyPolicy = false;
        public SettingsViewModel()
        {
            Title = "Settings";
            OpenTnC = new Command(() => 
            { 
                ShowTnC = true;
                ShowTnCButton = false;
            });
            OpenPrivacyPolicy = new Command(() => 
            { 
                ShowPrivacyPolicy = true;
                ShowPrivacyPolicyButton = false;
            });
        }

        public bool ShowTnCButton
        {
            get => _showTnCButton;
            set
            {
                _showTnCButton = value;
                OnPropertyChanged("ShowTnCButton");
            }
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

        public bool ShowPrivacyPolicyButton
        {
            get => _showPrivacyPolicyButton;
            set
            {
                _showPrivacyPolicyButton = value;
                OnPropertyChanged("ShowPrivacyPolicyButton");
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
            ShowTnCButton = true;
        }

        public void ClosePrivacyPolicy()
        {
            ShowPrivacyPolicy = false;
            ShowPrivacyPolicyButton = true;
        }
    }
}
