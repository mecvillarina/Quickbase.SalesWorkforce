using Acr.UserDialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.PubSubEvents;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class PurchaseOrdersPageViewModel : ViewModelBase
    {
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        public PurchaseOrdersPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            IPurchaseOrderManager purchaseOrderManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _purchaseOrderManager = purchaseOrderManager;
            Title = AppResources.TitlePurchaseOrders;

            TappedMenuCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerTappedEvent>().Publish());
            PurchaseOrders = new ObservableCollection<PurchaseOrderEntity>();

            AddCommand = new DelegateCommand(async () => await OnAdd());
        }

        private ObservableCollection<PurchaseOrderEntity> _purchaseOrders;
        public ObservableCollection<PurchaseOrderEntity> PurchaseOrders
        {
            get => _purchaseOrders;
            set => SetProperty(ref _purchaseOrders, value);
        }

        public DelegateCommand TappedMenuCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        private async Task OnAdd()
        {
            await PageNavigator.NavigateAsync(ViewNames.PurchaseOrderCreatePage);
        }

        
        private async Task FetchData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                var purchaseOrders = await RequestExceptionHandler.HandlerRequestTaskAsync(() => _purchaseOrderManager.GetPurchaseOrders());
                PurchaseOrders.Clear();

                foreach (var purchaseOrder in purchaseOrders)
                {
                    PurchaseOrders.Add(purchaseOrder);
                }

            }
            catch { }
            finally
            {

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
