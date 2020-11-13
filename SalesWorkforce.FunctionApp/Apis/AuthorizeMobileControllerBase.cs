using Microsoft.AspNetCore.Http;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class AuthorizeMobileControllerBase
    {
        public readonly ISalesAgentService SalesAgentService;
        public readonly IAccessTokenProvider AccessTokenProvider;

        public AuthorizeMobileControllerBase(ISalesAgentService salesAgentService, IAccessTokenProvider accessTokenProvider)
        {
            SalesAgentService = salesAgentService;
            AccessTokenProvider = accessTokenProvider;
        }

        public AccessTokenResult ValidateToken(HttpRequest req) => AccessTokenProvider.ValidateToken(req);

        public SalesAgentContract GetUser(AccessTokenResult tokenResult)
        {
            if (tokenResult.Status == AccessTokenStatus.Valid)
            {
                var userId = long.Parse(tokenResult.Principal.Claims.First(x => x.Type == "id").Value);
                return SalesAgentService.GetSalesAgent(userId);
            }

            return null;
        }
    }
}
