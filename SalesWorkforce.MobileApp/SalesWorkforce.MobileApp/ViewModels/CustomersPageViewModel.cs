using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.PubSubEvents;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class CustomersPageViewModel : ViewModelBase
    {
        private readonly ICustomerManager _customerManager;
        public CustomersPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            ICustomerManager customerManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _customerManager = customerManager;

            Title = AppResources.TitleCustomers;

            Customers = new ObservableCollection<CustomerEntity>();

            TappedMenuCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerTappedEvent>().Publish());
            AddCommand = new DelegateCommand(async () => await OnAdd());
        }

        private ObservableCollection<CustomerEntity> _customers;
        public ObservableCollection<CustomerEntity> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public DelegateCommand TappedMenuCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }

        private async Task OnAdd()
        {
            await PageNavigator.NavigateAsync(ViewNames.CustomerCreatePage);
        }

        private async Task FetchData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _customerManager.GetCustomers());
                Customers.Clear();

            }
            catch { }
            finally
            {
                var customers = _customerManager.GetCustomersLocally();
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }

                UserDialogs.HideLoading();
            }
        }


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await Task.Delay(100);
            await FetchData();
        }

    }
}
