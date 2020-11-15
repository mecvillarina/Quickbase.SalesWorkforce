using SalesWorkforce.MobileApp.Managers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Managers.Abstractions
{
    public interface IProductManager
    {
        Task GetProducts();
        List<ProductEntity> GetCustomersLocally();
        Task<ProductEntity> GetProduct(long recordId);
    }
}