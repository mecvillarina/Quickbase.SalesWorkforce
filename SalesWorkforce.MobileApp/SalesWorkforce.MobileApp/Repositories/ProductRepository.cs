using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.Base;
using SalesWorkforce.MobileApp.Repositories.DataObjects;

namespace SalesWorkforce.MobileApp.Repositories
{
    public class ProductRepository : Repository<ProductDataObject>, IProductRepository
    {
        public ProductRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
