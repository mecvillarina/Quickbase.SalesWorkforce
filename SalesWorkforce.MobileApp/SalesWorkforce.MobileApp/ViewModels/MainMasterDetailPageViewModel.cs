using Acr.UserDialogs;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Models;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class MainMasterDetailPageViewModel : MasterDetailViewModelBase
    {
        private readonly IAppManager _appManager;
        private readonly IAppUserManager _appUserManager;
        public MainMasterDetailPageViewModel(INavigationService navigationService,
            IPageNavigator pageNavigator,
            IUserDialogs userDialogs,
            IEventAggregator eventAggregator,
            IAppManager appManager,
            IAppUserManager appUserManager) : base(navigationService, pageNavigator, userDialogs, eventAggregator)
        {
            _appManager = appManager;
            _appUserManager = appUserManager;

            AccountCommmand = new DelegateCommand(async () => await OnAccount());
            AttendanceLogCommmand = new DelegateCommand(async () => await OnAttendanceLog());
            LogoutCommand = new DelegateCommand(async () => await OnLogout());
        }


        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string _displayNameInitial;
        public string DisplayNameInitial
        {
            get => _displayNameInitial;
            set => SetProperty(ref _displayNameInitial, value);
        }

        public DelegateCommand AttendanceLogCommmand { get; private set; }
        public DelegateCommand AccountCommmand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            SetProfile();
        }


        private Task OnAccount() => PageNavigator.NavigateAsync($"{ViewNames.NavigationPage}/{ViewNames.AccountPage}", null);

        private Task OnAttendanceLog() => PageNavigator.NavigateAsync($"{ViewNames.NavigationPage}/{ViewNames.AttendanceLogPage}", null);

        private async Task OnLogout()
        {
            IsPresented = false;
            var logoutResult = await UserDialogs.ConfirmAsync(AppResources.LabelSignoutMessage, AppResources.LabelSignoutTitle, AppResources.LabelYes, AppResources.LabelNo);

            if (logoutResult)
            {
                _appManager.ClearAll();
                await PageNavigator.NavigateAsync($"{ViewNames.ResetLandingPage()}");
            }
        }

        private void SetProfile()
        {
            var profile = _appUserManager.GetProfileLocally();
            DisplayName = profile.DisplayName;
            DisplayNameInitial = profile.DisplayNameInitial;
        }
    }
}
