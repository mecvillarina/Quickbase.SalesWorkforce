using SalesWorkforce.Common.DataContracts;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface ICustomerService
    {
        List<CustomerContract> GetCustomers(string query);
    }
}