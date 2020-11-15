using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using SalesWorkforce.MobileApp.WebServices.Base;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices
{
    public class CustomerWebService : WebServiceBase, ICustomerWebService
    {
        public CustomerWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<CustomerCollectionResponseContract> Search(string query, string accessToken)
        {
            string endpoint = string.Format(ServerEndpoint.CustomerQuery, query);
            return GetAsync<CustomerCollectionResponseContract>(endpoint, null, accessToken);
        }

        public Task<CustomerContract> Get(long recordId, string accessToken) => GetAsync<CustomerContract>(string.Format(ServerEndpoint.CustomerGet, recordId), null, accessToken);
        public Task<CustomerContract> Create(CustomerCreateRequestContract req, string accessToken) => PostAsync<CustomerContract>(ServerEndpoint.CustomerCreate, req, null, accessToken);
    }
}
