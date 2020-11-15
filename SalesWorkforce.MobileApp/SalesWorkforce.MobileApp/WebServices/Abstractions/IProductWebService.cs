using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
    public interface IProductWebService
    {
        Task<ProductCollectionResponseContract> Search(string query, string accessToken);
        Task<ProductContract> Get(long recordId, string accessToken);
    }
}