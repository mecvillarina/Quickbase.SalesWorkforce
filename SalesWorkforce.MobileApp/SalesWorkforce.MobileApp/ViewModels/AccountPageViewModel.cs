using Acr.UserDialogs;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.PubSubEvents;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly IAppUserManager _appUserManager;
        public AccountPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            IAppUserManager appUserManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _appUserManager = appUserManager;
            Title = AppResources.TitleAccount;
            TappedMenuCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerTappedEvent>().Publish());

        }


        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _agentId;
        public string AgentId
        {
            get => _agentId;
            set => SetProperty(ref _agentId, value);
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }

        private string _attendanceType;
        public string AttendanceType
        {
            get => _attendanceType;
            set => SetProperty(ref _attendanceType, value);
        }

        public DelegateCommand TappedMenuCommand { get; private set; }

        private void SetProfile()
        {
            var profile = _appUserManager.GetProfileLocally();
            FirstName = profile.FirstName;
            MiddleName = profile.MiddleName;
            LastName = profile.LastName;
            AgentId = profile.AgentId;
            EmailAddress = profile.EmailAddress;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SetProfile();
            EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Publish(true);
        }
    }
}
