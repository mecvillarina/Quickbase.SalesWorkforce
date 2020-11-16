using Prism;
using Prism.Ioc;
using SalesWorkforce.MobileApp.ViewModels;
using SalesWorkforce.MobileApp.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using SalesWorkforce.MobileApp.Managers;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.WebServices;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using SalesWorkforce.MobileApp.Repositories;
using SalesWorkforce.MobileApp.Repositories.Database;
using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Utilities.Abstractions;
using SalesWorkforce.MobileApp.Utilities;
using SalesWorkforce.MobileApp.WebServices.Utilities;
using SalesWorkforce.MobileApp.Managers.Mappers;
using Acr.UserDialogs;
using SalesWorkforce.MobileApp.Common.Constants;

namespace SalesWorkforce.MobileApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{ViewNames.NavigationPage}/{ViewNames.SplashScreenPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterPlugins(containerRegistry);
            RegisterUtilites(containerRegistry);
            RegisterRepositories(containerRegistry);
            RegisterWebServices(containerRegistry);
            RegisterManagers(containerRegistry);
            RegisterUI(containerRegistry);

        }

        private void RegisterUI(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SplashScreenPage, SplashScreenPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage, AccountPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomersPage, CustomersPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomerCreatePage, CustomerCreatePageViewModel>();
        }

        private void RegisterManagers(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppManager, AppManager>();
            containerRegistry.RegisterSingleton<IInternalAuthManager, InternalAuthManager>();
            containerRegistry.RegisterSingleton<IAuthManager, AuthManager>();
            containerRegistry.RegisterSingleton<IAppUserManager, AppUserManager>();
            containerRegistry.RegisterSingleton<ICustomerManager, CustomerManager>();
            containerRegistry.RegisterSingleton<IProductManager, ProductManager>();
            containerRegistry.RegisterSingleton<IPurchaseOrderManager, PurchaseOrderManager>();
        }

        private void RegisterWebServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IHttpService, HttpService>();
            containerRegistry.Register<IAppHttpClient, AppHttpClient>();
            containerRegistry.RegisterSingleton<IAuthWebService, AuthWebService>();
            containerRegistry.RegisterSingleton<ICustomerWebService, CustomerWebService>();
            containerRegistry.RegisterSingleton<IProductWebService, ProductWebService>();
            containerRegistry.RegisterSingleton<IPurchaseOrderWebService, PurchaseOrderWebService>();
        }

        private void RegisterRepositories(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMobileDatabase, MobileDatabase>();
            containerRegistry.RegisterSingleton<IKeyStoreRepository, KeyStoreRepository>();
            containerRegistry.RegisterSingleton<IInternalAuthRepository, InternalAuthRepository>();
            containerRegistry.RegisterSingleton<IAppUserRepository, AppUserRepository>();
            containerRegistry.RegisterSingleton<ICustomerRepository, CustomerRepository>();
            containerRegistry.RegisterSingleton<IProductRepository, ProductRepository>();
        }

        private void RegisterUtilites(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IConnectivity, ConnectivityImplementation>();
            containerRegistry.RegisterSingleton<IPermissions, PermissionsImplementation>();
            containerRegistry.RegisterSingleton<IGeolocation, GeolocationImplementation>();
            containerRegistry.RegisterSingleton<IPreferences, PreferencesImplementation>();
            containerRegistry.RegisterSingleton<ISecureStorage, SecureStorageImplementation>();
            containerRegistry.RegisterSingleton<ILogger, Logger>();
            containerRegistry.RegisterSingleton<IHttpMessageHelper, HttpMessageHelper>();
            containerRegistry.RegisterSingleton<IJsonHelper, JsonHelper>();
            containerRegistry.RegisterSingleton<IRequestExceptionHandler, RequestExceptionHandler>();

            containerRegistry.RegisterSingleton<IServiceMapper, ServiceMapper>();
            containerRegistry.RegisterSingleton<IAppCenterLogger, AppCenterLogger>();
            containerRegistry.RegisterSingleton<IDebugLogger, DebugLogger>();
            containerRegistry.Register<IPageNavigator, AppPageNavigator>();
        }

        private void RegisterPlugins(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(UserDialogs.Instance);
        }
    }
}
