using Microsoft.Extensions.Options;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public List<ProductContract> GetProducts(string query)
        {
            var queryRequest = new QueryRequestModel("bqy89m7q2");
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };

            if (!string.IsNullOrEmpty(query))
            {
                queryRequest.Where = $"{{6.CT.'{query}'}}OR{{7.CT.'{query}'}}";
            }

            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var products = new List<ProductContract>();
            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    products.Add(new ProductContract(data));
                }
            }

            return products;
        }

        public ProductContract GetProduct(long recordId)
        {
            var queryRequest = new QueryRequestModel("bqy89m7q2");
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11 };
            queryRequest.Where = $"{{3.EX.{recordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var products = new List<ProductContract>();
            if (result.Data != null)
            {
                var data = result.Data.First();
                return new ProductContract(data);
            }

            return null;
        }
    }
}
