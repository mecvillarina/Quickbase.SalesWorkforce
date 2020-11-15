using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using SalesWorkforce.MobileApp.WebServices.Base;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices
{
    public class ProductWebService : WebServiceBase, IProductWebService
    {
        public ProductWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<ProductCollectionResponseContract> Search(string query, string accessToken)
        {
            string endpoint = string.Format(ServerEndpoint.ProductQuery, query);
            return GetAsync<ProductCollectionResponseContract>(endpoint, null, accessToken);
        }

        public Task<ProductContract> Get(long recordId, string accessToken) => GetAsync<ProductContract>(string.Format(ServerEndpoint.ProductGet, recordId), null, accessToken);
    }
}
