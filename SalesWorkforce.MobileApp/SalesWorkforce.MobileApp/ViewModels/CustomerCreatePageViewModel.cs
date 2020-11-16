using Acr.UserDialogs;
using Prism.Commands;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using System;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class CustomerCreatePageViewModel : ViewModelBase
    {
        private readonly ICustomerManager _customerManager;
        public CustomerCreatePageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            ICustomerManager customerManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
            _customerManager = customerManager;

            BackCommand = new DelegateCommand(async () => await PageNavigator.GoBackAsync());
            SubmitCommand = new DelegateCommand(async () => await OnSubmit(), () => OnSubmitCanExecute());

            Title = AppResources.TitleAddCustomer;
        }

        private bool _isSubmitCommandEnabled;
        public bool IsSubmitCommandEnabled
        {
            get => _isSubmitCommandEnabled;
            set => SetProperty(ref _isSubmitCommandEnabled, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                OnSubmitCanExecute();
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value);
                OnSubmitCanExecute();
            }
        }

        private string _contactNumber;
        public string ContactNumber
        {
            get => _contactNumber;
            set
            {
                SetProperty(ref _contactNumber, value);
                OnSubmitCanExecute();
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                SetProperty(ref _emailAddress, value);
                OnSubmitCanExecute();
            }
        }

        private string _billingAddress;
        public string BillingAddress
        {
            get => _billingAddress;
            set
            {
                SetProperty(ref _billingAddress, value);
                OnSubmitCanExecute();
            }
        }

        private string _deliveryAddress;
        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set
            {
                SetProperty(ref _deliveryAddress, value);
                OnSubmitCanExecute();
            }
        }

        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private bool OnSubmitCanExecute()
        {
            IsSubmitCommandEnabled = !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Address) &&
                !string.IsNullOrEmpty(ContactNumber) &&
                !string.IsNullOrEmpty(EmailAddress) &&
                !string.IsNullOrEmpty(BillingAddress) &&
                !string.IsNullOrEmpty(DeliveryAddress);

            return IsSubmitCommandEnabled;
        }

        private async Task OnSubmit()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingSubmitting);

                var req = new CustomerCreateRequestEntity()
                {
                    Name = Name,
                    Address = Address,
                    Email = EmailAddress,
                    ContactNumber = ContactNumber,
                    BillingAddress = BillingAddress,
                    DeliveryAddress = DeliveryAddress
                };

                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _customerManager.Create(req));
                UserDialogs.Toast(AppResources.LabelSuccessCreateCustomer);
                await PageNavigator.GoBackAsync();
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

        public override void OnNavigatedTo(Prism.Navigation.INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

#if DEBUG
            Name = "John Doe";
            Address = "Manila, Philippines";
            ContactNumber = "+639989877862";
            EmailAddress = "john.doe@gmail.com";
            BillingAddress = "Manila, Philippines";
            DeliveryAddress = "Manila, Philippines";
#endif
        }
    }
}
