using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.ViewModel
{
    public class DebugViewModel
    {
        public int screenWidth, screenHeight;
        public int hexRows = 5;
        public int hexColumns = 5;

        private readonly IPageService _pageService;

        public DebugViewModel(IPageService pageService)
        {
            _pageService = pageService;
        }



    }
}
