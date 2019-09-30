using MvvmHelpers;

/// <summary>
/// @Roshens page
/// </summary>
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
