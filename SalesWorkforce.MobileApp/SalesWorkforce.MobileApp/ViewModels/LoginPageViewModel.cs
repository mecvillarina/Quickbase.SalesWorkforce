using Acr.UserDialogs;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Plugin.AzurePushNotification;
using System.Collections.Generic;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAzurePushNotification _azurePushNotification;
        private readonly IAuthManager _authManager;
        private readonly IAppUserManager _appUserManager;

        public LoginPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IAzurePushNotification azurePushNotification,
            IAuthManager authManager,
            IAppUserManager appUserManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
            _azurePushNotification = azurePushNotification;
            _authManager = authManager;
            _appUserManager = appUserManager;

            LoginCommand = new DelegateCommand(async () => await OnLogin(), () => OnLoginCanExecute())
                .ObservesProperty(() => AgentId)
                .ObservesProperty(() => BadgeCode);
        }

        private string _agentId = string.Empty;
        public string AgentId
        {
            get => _agentId;
            set => SetProperty(ref _agentId, value);
        }

        private string _badgeCode = string.Empty;
        public string BadgeCode
        {
            get => _badgeCode;
            set => SetProperty(ref _badgeCode, value);
        }

        public DelegateCommand LoginCommand { get; private set; }

        public bool OnLoginCanExecute()
        {
            return !string.IsNullOrEmpty(AgentId) && !string.IsNullOrEmpty(BadgeCode);
        }

        private async Task OnLogin()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingLoggingIn);

                var req = new AuthLoginRequestEntity()
                {
                    AgentId = AgentId,
                    BadgeCode = BadgeCode
                };

                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _authManager.Login(req));
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _appUserManager.GetProfile());

                var profile = _appUserManager.GetProfileLocally();

                List<string> tags = new List<string>();
                tags.Add(profile.RecordId.ToString());

                //_azurePushNotification.RegisterForPushNotifications();
                await _azurePushNotification.RegisterAsync(tags.ToArray());
                await PageNavigator.NavigateAsync($"../{ViewNames.GetMainMasterPage()}");
            }
            catch (NoInternetConnectivityException)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_NoInternetConnectivity, string.Empty, AppResources.ButtonOk);
            }
            catch (DomainException ex)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(ex.Message, string.Empty, AppResources.ButtonOk);
            }
            catch (InvalidAuthTokenException)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_SessionExpireMessage, AppResources.Error_SessionExpireTitle);
                await PageNavigator.ForceLogout();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.AppCenterLogError(ex);
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_DefaultServerError, string.Empty, AppResources.ButtonOk);
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                _authManager.ClearAuthData();
            }

#if DEBUG
            AgentId = "1000";
            BadgeCode = "235346";
#endif
        }
    }
}
