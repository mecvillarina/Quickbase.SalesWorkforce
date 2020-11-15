using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
    public interface ICustomerWebService
    {
        Task<CustomerCollectionResponseContract> Search(string query, string accessToken);
        Task<CustomerContract> Get(long recordId, string accessToken);
        Task<CustomerContract> Create(CustomerCreateRequestContract req, string accessToken);

    }
}