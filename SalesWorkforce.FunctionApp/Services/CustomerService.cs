using Microsoft.Extensions.Options;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        public CustomerService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public List<CustomerContract> GetCustomers(string query)
        {
            var queryRequest = new QueryRequestModel("bqzamrfvt");
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };

            if (!string.IsNullOrEmpty(query))
            {
                queryRequest.Where = $"{{6.CT.'{queryRequest}'}}";
            }
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var customers = new List<CustomerContract>();
            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    customers.Add(new CustomerContract()
                    {
                        RecordId = (long)data["3"].Value,
                        Name = data["6"].Value.ToString(),
                        Address = data["7"].Value.ToString(),
                        ContactNumber = data["8"].Value.ToString(),
                        Email = data["9"].Value.ToString(),
                        BillingAddress = data["10"].Value.ToString(),
                        DeliveryAddress = data["11"].Value.ToString(),
                    });
                }

            }

            return customers;
        }
    }
}
