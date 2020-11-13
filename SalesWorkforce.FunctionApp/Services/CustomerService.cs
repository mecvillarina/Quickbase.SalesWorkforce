using Microsoft.Extensions.Options;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Services
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly string _dbId = "bqzamrfvt";
        public CustomerService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {

        }

        public long? AddCustomer(CustomerCreateRequestContract reqContract)
        {
            var row = new Dictionary<string, Datum>();
            row.Add("6", new Datum() { Value = reqContract.Name });
            row.Add("7", new Datum() { Value = reqContract.Address });
            row.Add("8", new Datum() { Value = reqContract.ContactNumber });
            row.Add("9", new Datum() { Value = reqContract.Email });
            row.Add("10", new Datum() { Value = reqContract.BillingAddress });
            row.Add("11", new Datum() { Value = reqContract.DeliveryAddress });

            var postRequest = new PostRequestModel(_dbId);
            postRequest.FieldsToReturn = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };
            postRequest.Data.Add(row);

            var result = PostRequest<PostRequestModel, PostResponseModel>("/v1/records", postRequest);

            if (result.Data.Any())
            {
                return (long)result.Data.First()["3"].Value;
            }

            return null;
        }

        public List<CustomerContract> GetCustomers(string query)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };

            if (!string.IsNullOrEmpty(query))
            {
                queryRequest.Where = $"{{6.CT.'{query}'}}";
            }
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var customers = new List<CustomerContract>();
            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    customers.Add(new CustomerContract(data));
                }

            }

            return customers;
        }

        public CustomerContract GetCustomer(long recordId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };
            queryRequest.Where = $"{{3.EX.{recordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);
            if (result.Data != null)
            {
                var data = result.Data.First();
                return new CustomerContract(data);
            }

            return null;
        }
    }
}
