using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
    public interface IAuthWebService
    {
        Task<AuthTokenDataContract> Login(AuthLoginRequestContract contract);
        Task<SalesAgentContract> GetProfile(string accessToken);
    }
}