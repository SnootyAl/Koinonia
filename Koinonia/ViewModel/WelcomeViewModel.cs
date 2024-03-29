﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Koinonia.Views;

/// <summary>
/// Landing page for the app when it is opened the first time. Skipped once profile exists.
/// </summary>
namespace Koinonia.ViewModel
{
    class WelcomeViewModel
    {
        public int screenWidth, screenHeight;
        public int buttonSize = 10;

        private readonly IPageService _pageService;
        public ICommand StartNowCommand { get; private set; }



        public WelcomeViewModel(IPageService pageService)
        {
            _pageService = pageService;
            StartNowCommand = new Command(StartNow);
        }

        private async void StartNow()
        {
            
            await _pageService.PushAsync(new SignupPage());
        }

    }

    
}
