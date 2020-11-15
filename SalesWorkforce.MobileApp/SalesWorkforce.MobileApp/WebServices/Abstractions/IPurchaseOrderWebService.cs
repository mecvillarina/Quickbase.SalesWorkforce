using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
    public interface IPurchaseOrderWebService
    {
        Task<PurchaseOrderCollectionResponseContract> GetAll(string accessToken);
        Task<PurchaseOrderContract> Get(long recordId, string accessToken);
        Task<PurchaseOrderContract> Create(PurchaseOrderCreateRequestContract req, string accessToken);
    }
}