using Microsoft.Extensions.Options;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Services
{
    public class SalesAgentService : ServiceBase, ISalesAgentService
    {
        private readonly string _dbId = "bqzakyuwb";

        public SalesAgentService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public SalesAgentContract GetSalesAgent(long recordId)
        {
            var query = new QueryRequestModel(_dbId);
            query.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12 };
            query.Where = $"{{3.EX.{recordId}}}";
            query.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", query);

            if (result.Data.Any())
            {
                var data = result.Data.First();
                return new SalesAgentContract(data);
            }

            return null;
        }
    }
}
