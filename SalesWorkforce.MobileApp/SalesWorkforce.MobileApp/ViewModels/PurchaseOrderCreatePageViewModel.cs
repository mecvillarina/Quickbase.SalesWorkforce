using Acr.UserDialogs;
using Prism.Commands;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Localization;
using SalesWorkforce.MobileApp.Managers;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Models;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.ViewModels
{
    public class PurchaseOrderCreatePageViewModel : ViewModelBase
    {
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        private readonly ICustomerManager _customerManager;
        private readonly IProductManager _productManager;

        public PurchaseOrderCreatePageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IPurchaseOrderManager purchaseOrderManager,
            ICustomerManager customerManager,
            IProductManager productManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
            _purchaseOrderManager = purchaseOrderManager;
            _customerManager = customerManager;
            _productManager = productManager;

            BackCommand = new DelegateCommand(async () => await PageNavigator.GoBackAsync());
            SubmitCommand = new DelegateCommand(async () => await OnSubmit(), () => OnSubmitCanExecute());
            SelectCustomerCommand = new DelegateCommand(async () => await OnSelectCustomer());
            AddProductCommand = new DelegateCommand(async () => await OnAddProduct());

            Title = AppResources.TitleCreatePurchaseOrder;

            _products = new List<ProductEntity>();
            PurchaseOrderProducts = new ObservableCollection<PurchaseOrderProductItemModel>();
        }

        private List<ProductEntity> _products { get; set; }
        private ProductEntity _selectedProduct { get; set; }

        private bool _isSubmitCommandEnabled;
        public bool IsSubmitCommandEnabled
        {
            get => _isSubmitCommandEnabled;
            set => SetProperty(ref _isSubmitCommandEnabled, value);
        }

        private CustomerEntity _selectedCustomer;
        public CustomerEntity SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                OnSubmitCanExecute();
            }
        }

        private ObservableCollection<PurchaseOrderProductItemModel> _purchaseOrderProducts;
        public ObservableCollection<PurchaseOrderProductItemModel> PurchaseOrderProducts
        {
            get => _purchaseOrderProducts;
            set => SetProperty(ref _purchaseOrderProducts, value);
        }

        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand SelectCustomerCommand { get; private set; }
        public DelegateCommand AddProductCommand { get; private set; }

        private bool OnSubmitCanExecute()
        {
            IsSubmitCommandEnabled = SelectedCustomer != null;

            return IsSubmitCommandEnabled;
        }

        private async Task OnSubmit()
        {
            if (SelectedCustomer == null)
            {
                await UserDialogs.AlertAsync("Please select a customer.");
            }
            else if (!_products.Any())
            {
                await UserDialogs.AlertAsync("Please add at least one product.");
            }
            else
            {
                try
                {
                    UserDialogs.ShowLoading(AppResources.LoadingSubmitting);

                    var req = new PurchaseOrderCreateRequestEntity()
                    {
                        CustomerRecordId = SelectedCustomer.RecordId,
                        Products = PurchaseOrderProducts.Select(x => new PurchaseOrderProductCreateRequestEntity()
                        {
                            ProductRecordId = x.Product.RecordId,
                            Quantity = x.Quantity
                        }).ToList()
                    };

                    await RequestExceptionHandler.HandlerRequestTaskAsync(() => _purchaseOrderManager.Create(req));
                    UserDialogs.Toast(AppResources.LabelSuccessCreatePurchaseOrder);
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

        }

        private async Task OnSelectCustomer()
        {
            await FetchCustomerData();
        }

        private async Task OnAddProduct()
        {
            ActionSheetConfig config = new ActionSheetConfig();
            foreach (var product in _products)
            {
                config.Add($"{product.SKU} - {product.Name} - {product.BasePrice}", () =>
                {
                    _selectedProduct = product;
                    var promptConfig = new PromptConfig();
                    promptConfig.InputType = InputType.Number;
                    promptConfig.Title = "Enter quantity";
                    promptConfig.Text = "0";
                    promptConfig.OnAction = delegate (PromptResult promptResult)
                    {
                        if (promptResult.Ok)
                        {
                            var quantity = 0.0;

                            if (double.TryParse(promptResult.Text, out quantity))
                            {
                                if (quantity > 0)
                                {
                                    PurchaseOrderProducts.Add(new PurchaseOrderProductItemModel()
                                    {
                                        Product = _selectedProduct,
                                        Quantity = quantity
                                    });
                                }
                            }
                        }
                    };

                    UserDialogs.Prompt(promptConfig);
                });
            }

            UserDialogs.ActionSheet(config);
        }

        private async Task FetchCustomerData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _customerManager.GetCustomers());
                UserDialogs.HideLoading();

                var customers = _customerManager.GetCustomersLocally();

                if (!customers.Any())
                {
                    UserDialogs.Toast(AppResources.LabelAddCustomerFirst);
                    await PageNavigator.GoBackAsync();
                }

                ActionSheetConfig config = new ActionSheetConfig();
                config.Title = "Select a customer";
                foreach (var customer in customers)
                {
                    config.Add(customer.Name, () => SelectedCustomer = customer);
                }

                UserDialogs.ActionSheet(config);
            }
            catch
            {

            }
        }

        private async Task FetchProductData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _productManager.GetProducts());
                UserDialogs.HideLoading();

                _products = _productManager.GetProductsLocally();
            }
            catch
            {

            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        public async override void OnNavigatedTo(Prism.Navigation.INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await FetchCustomerData();
            await FetchProductData();
        }
    }

}
