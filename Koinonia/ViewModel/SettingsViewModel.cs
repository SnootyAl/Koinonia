using MvvmHelpers;


namespace Koinonia.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        public SettingsViewModel(IPageService pageService)
        {
            _pageService = pageService;
        }
    }
}
