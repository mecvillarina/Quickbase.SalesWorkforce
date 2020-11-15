using SalesWorkforce.MobileApp.Utilities.Abstractions;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IPageNavigator pageNavigator, ILogger logger) : base(pageNavigator, logger)
        {
        }
    }
}
