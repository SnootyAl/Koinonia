using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.ViewModel
{
    class TagsViewModel
    {
        private readonly IPageService _pageService;
        public TagsViewModel(PageService pageService)
        {
            _pageService = pageService;
        }
    }
}
