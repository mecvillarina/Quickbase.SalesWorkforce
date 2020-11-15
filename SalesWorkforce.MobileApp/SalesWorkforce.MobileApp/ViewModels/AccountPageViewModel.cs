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

            ClockInCommand = new DelegateCommand(async () => await OnClock("Clock In"));
            ClockOutCommand = new DelegateCommand(async () => await OnClock("Clock Out"));
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

        private string _employeeId;
        public string EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }

        private string _attendanceType;
        public string AttendanceType
        {
            get => _attendanceType;
            set => SetProperty(ref _attendanceType, value);
        }

        public DelegateCommand TappedMenuCommand { get; private set; }
        public DelegateCommand ClockInCommand { get; private set; }
        public DelegateCommand ClockOutCommand { get; private set; }

        private async Task OnClock(string logType)
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavParameters.LogType, logType);

            await PageNavigator.NavigateAsync(ViewNames.ClockPage, parameters);
        }

        private void SetProfile()
        {
            var profile = _appUserManager.GetProfileLocally();
            FirstName = profile.FirstName;
            MiddleName = profile.MiddleName;
            LastName = profile.LastName;
            //EmployeeId = profile.EmployeeID;
            //AttendanceType = profile.UseGeofencing ? AppResources.LabelGeofencing : AppResources.LabelGeotagging;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SetProfile();
            EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Publish(true);
        }
    }
}
