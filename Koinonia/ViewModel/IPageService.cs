using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/// <summary>
/// Breaks out the Navigation and display aspects into their own class for further separation of concerns.
/// </summary>
namespace Koinonia.ViewModel
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task PopAsync();
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel = null);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction = null, params string[] buttons);


    }
}
