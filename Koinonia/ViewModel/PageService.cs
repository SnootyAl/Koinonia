using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    public class PageService : IPageService
    {
        //Extract Mainpage

        //Helper functions implementing Xamarin commands, to remove usage from ViewModels
        public async Task<bool> DisplayAlert(string title, string message, string cancel, string ok = null)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
            
        }
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction = null, 
                                                        params string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, null, buttons);
        }
    
        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        
    }
}
