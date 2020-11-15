using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using SalesWorkforce.MobileApp.WebServices.Base;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices
{
    public class AuthWebService : WebServiceBase, IAuthWebService
    {
        public AuthWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<AuthTokenDataContract> Login(AuthLoginRequestContract contract) => PostAsync<AuthTokenDataContract>(ServerEndpoint.AuthLogin, contract);
        public Task<SalesAgentContract> GetProfile(string accessToken) => GetAsync<SalesAgentContract>(ServerEndpoint.AuthGetProfile, null, accessToken);
    }
}
