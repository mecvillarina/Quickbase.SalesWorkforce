using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface ICustomerService
    {
        long? AddCustomer(CustomerCreateRequestContract reqContract);
        List<CustomerContract> GetCustomers(string query);
        CustomerContract GetCustomer(long recordId);
    }
}