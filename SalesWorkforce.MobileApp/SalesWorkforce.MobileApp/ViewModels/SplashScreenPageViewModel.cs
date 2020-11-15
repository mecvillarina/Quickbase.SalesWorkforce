using Acr.UserDialogs;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using Prism.Navigation;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class SplashScreenPageViewModel : ViewModelBase
    {
        private readonly IAuthManager _authManager;
        public SplashScreenPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IAuthManager authManager) : base(pageNavigator, logger, userDialogs)
        {
            _authManager = authManager;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await Task.Delay(1000);

            var isSessionValid = await _authManager.IsSessionValid();

            if (isSessionValid)
            {
                await PageNavigator.NavigateAsync($"../{ViewNames.GetMainMasterPage()}");
                return;
            }

            await PageNavigator.NavigateAsync($"../{ViewNames.LoginPage}");
        }
    }
}
