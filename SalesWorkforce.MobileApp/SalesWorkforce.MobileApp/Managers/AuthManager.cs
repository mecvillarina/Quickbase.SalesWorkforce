using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class AuthManager : AuthenticatedManagerBase, IAuthManager
    {
        private readonly IInternalAuthManager _internalAuthManager;
        private readonly IAuthWebService _authWebService;

        public AuthManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager internalAuthManager,
            IAuthWebService authWebService) : base(connectivity, mapper, internalAuthManager)
        {
            _internalAuthManager = internalAuthManager;
            _authWebService = authWebService;
        }

        public void ClearAuthData() => _internalAuthManager.ClearAuthData();
        public Task<bool> IsSessionValid() => _internalAuthManager.IsSessionValid();

        public async Task Login(AuthLoginRequestEntity reqEntity)
        {
            EnsureInternetAvailable();

            try
            {
                var reqContract = Mapper.Map<AuthLoginRequestContract>(reqEntity);
                var authToken = await _authWebService.Login(reqContract);
                await _internalAuthManager.SaveSessionToken(authToken.AccessToken, authToken.ExpireAt);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

      
    }
}
