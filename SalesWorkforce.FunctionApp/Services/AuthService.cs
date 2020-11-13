using Microsoft.Extensions.Options;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly string _dbId = "bqzakyuwb";

        public AuthService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public long? Login(string agentId, string badgeCode)
        {
            var query = new QueryRequestModel(_dbId);
            query.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12 };
            query.Where = $"{{9.EX.'{agentId}'}}AND{{10.EX.'{badgeCode}'}}";
            query.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", query);

            if (result.Data.Any())
            {
                return (long)result.Data.First()["3"].Value;
            }

            return null;
        }
    }
}
