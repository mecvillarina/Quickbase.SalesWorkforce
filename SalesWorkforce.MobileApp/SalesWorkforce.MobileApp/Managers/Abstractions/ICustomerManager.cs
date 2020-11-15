using SalesWorkforce.MobileApp.Managers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Managers.Abstractions
{
    public interface ICustomerManager
    {
        Task GetCustomers();
        List<CustomerEntity> GetCustomersLocally();
        Task<CustomerEntity> GetCustomer(long recordId);
        Task<CustomerEntity> Create(CustomerCreateRequestEntity reqEntity);
    }
}