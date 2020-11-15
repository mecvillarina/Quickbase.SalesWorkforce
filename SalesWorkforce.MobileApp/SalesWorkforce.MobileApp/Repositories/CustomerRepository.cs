using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.Base;
using SalesWorkforce.MobileApp.Repositories.DataObjects;

namespace SalesWorkforce.MobileApp.Repositories
{
    public class CustomerRepository : Repository<CustomerDataObject>, ICustomerRepository
    {
        public CustomerRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
