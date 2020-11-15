using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Common.Constants;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using SalesWorkforce.MobileApp.WebServices.Base;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices
{
    public class PurchaseOrderWebService : WebServiceBase, IPurchaseOrderWebService
    {
        public PurchaseOrderWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<PurchaseOrderCollectionResponseContract> GetAll(string accessToken) => GetAsync<PurchaseOrderCollectionResponseContract>(ServerEndpoint.PurchaseOrderGetAll, null, accessToken);

        public Task<PurchaseOrderContract> Get(long recordId, string accessToken) => GetAsync<PurchaseOrderContract>(string.Format(ServerEndpoint.PurchaseOrderGet, recordId), null, accessToken);

        public Task<PurchaseOrderContract> Create(PurchaseOrderCreateRequestContract req, string accessToken) => PostAsync<PurchaseOrderContract>(ServerEndpoint.PurchaseOrderCreate, req, null, accessToken);

    }
}
