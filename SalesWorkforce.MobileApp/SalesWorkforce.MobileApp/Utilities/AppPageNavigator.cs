using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using Prism.Events;
using Prism.Navigation;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Utilities
{
    public class AppPageNavigator : PageNavigator, IPageNavigator
    {
        private readonly IAppManager _appManager;

        public AppPageNavigator(INavigationService navigationService,
            IEventAggregator eventAggregator,
            IInternalAuthManager authManager,
            IAppManager appManager) : base(navigationService, eventAggregator, authManager)
        {
            _appManager = appManager;
        }

        public async Task ForceLogout()
        {
            _appManager.ClearAll();
            //await NavigateAsync($"/{ViewNames.NavigationPage}/{ViewNames.SplashScreenPage}");
        }
    }
}
