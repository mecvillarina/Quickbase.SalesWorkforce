using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.DataObjects;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class AppUserManager : AuthenticatedManagerBase, IAppUserManager
    {
        private readonly IAuthWebService _authWebService;
        private readonly IAppUserRepository _appUserRepository;

        public AppUserManager(IConnectivity connectivity,
            IServiceMapper mapper, 
            IInternalAuthManager authManager,
            IAuthWebService authWebService,
            IAppUserRepository appUserRepository) : base(connectivity, mapper, authManager)
        {
            _authWebService = authWebService;
            _appUserRepository = appUserRepository;
        }

        public async Task GetProfile()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _authWebService.GetProfile(accessToken);
                var dataObject = Mapper.Map<AppUserDataObject>(contract);
                _appUserRepository.Clear();
                _appUserRepository.Add(dataObject);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public AppUserEntity GetProfileLocally()
        {
            var dataObject = _appUserRepository.FirstOrDefault(x => true);
            return Mapper.Map<AppUserEntity>(dataObject);
        }
    }
}
