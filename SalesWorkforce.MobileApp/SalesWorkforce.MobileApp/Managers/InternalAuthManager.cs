using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.DataObjects;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class InternalAuthManager : ManagerBase, IInternalAuthManager
    {
        private readonly IInternalAuthRepository _authRepository;
        private readonly IPreferences _preferences;

        public InternalAuthManager(IConnectivity connectivity, 
            IServiceMapper mapper,
            IInternalAuthRepository authRepository,
            IPreferences preferences)
            : base(connectivity, mapper)
        {
            _authRepository = authRepository;
            _preferences = preferences;
        }

        public async Task SaveSessionToken(string accessToken, long expiresAt)
        {
            ClearAuthData();
            await _authRepository.SetTokenPayload(new AuthDataObject()
            {
                AccessToken = accessToken,
                ExpireAt = expiresAt
            });

            _preferences.Set(PreferenceKeys.IsAuthenticated, true);
        }

        public async Task<bool> IsSessionValid()
        {
            return _preferences.Get(PreferenceKeys.IsAuthenticated, false) && await _authRepository.IsTokenValid();
        }

        public async Task EnsureSessionIsValid()
        {
            var isSessionValid = await _authRepository.IsTokenValid();
            var isAuthenticated = _preferences.Get(PreferenceKeys.IsAuthenticated, false);

            if (!isSessionValid || !isAuthenticated)
                throw new InvalidAuthTokenException();
        }

        public void ClearAuthData()
        {
            _authRepository.ClearTokenPayload();
            _preferences.Remove(PreferenceKeys.IsAuthenticated);
        }

        public Task<string> GetAuthToken() => _authRepository.GetToken();
    }
}
