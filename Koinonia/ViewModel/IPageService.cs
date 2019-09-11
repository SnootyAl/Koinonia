﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
