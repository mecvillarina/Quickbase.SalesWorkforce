using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Repositories.Abstractions;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class AppManager : ManagerBase, IAppManager
    {
        private readonly IPreferences _preferences;
        private readonly IMobileDatabase _mobileDatabase;
        private readonly ISecureStorage _secureStorage;

        public AppManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IPreferences preferences,
            IMobileDatabase mobileDatabase,
            ISecureStorage secureStorage) : base(connectivity, mapper)
        {
            _preferences = preferences;
            _mobileDatabase = mobileDatabase;
            _secureStorage = secureStorage;
        }

        public void ClearAll()
        {
            _preferences.Clear();
            _secureStorage.RemoveAll();
            _mobileDatabase.DeleteAll();
        }
    }
}
