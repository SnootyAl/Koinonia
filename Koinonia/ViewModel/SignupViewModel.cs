using Koinonia.Models;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Koinonia.Views;

namespace Koinonia.ViewModel
{
    public class SignupViewModel : BaseViewModel
    {
        public Profile Profile { get; set; }
        private readonly IPageService _pageService; 
        public ICommand NextButtonCommand { get; private set; }


        public SignupViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Profile = new Profile()
            {
                ContactID = 0
            };
            NextButtonCommand = new Command(Next);
        }   
             

        private async void Next()
        {
                      
            await _pageService.DisplayAlert(Profile.FirstName, Profile.ContactID.ToString(), "OK", "Cancel");
            var x = await App.Database.SaveProfileAsync(Profile);
            var recprds = await App.Database.GetProfileAsync(0);
            //Need to figure out preferences in MVVM model
            //App.Current.Preferences.Set("ProfileExists", true);
            await _pageService.PushAsync(new ContactList());
        }

        
    }

    
}
