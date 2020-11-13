using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.Common.DataContracts.Responses;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class AuthController : AuthorizeMobileControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(ISalesAgentService salesAgentService, IAccessTokenProvider accessTokenProvider, IAuthService authService) : base(salesAgentService, accessTokenProvider)
        {
            _authService = authService;
        }

        [FunctionName("AuthLogin")]
        public IActionResult Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/login")] AuthLoginRequestContract contract)
        {
            if (string.IsNullOrEmpty(contract.BadgeCode) || string.IsNullOrEmpty(contract.AgentId))
            {
                return new BadRequestResult();
            }

            var id = _authService.Login(contract.AgentId, contract.BadgeCode);

            if (id.HasValue)
            {
                var jwtResult = AccessTokenProvider.GenerateToken(id.ToString());
                return new OkObjectResult(jwtResult);
            }

            return new BadRequestObjectResult(new BadRequestResponseContract() { Message = "Invalid Username or password." });
        }

        [FunctionName("AuthGetProfile")]
        public IActionResult GetProfile([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "auth/me")] HttpRequest req)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);

            if (user != null)
            {
                return new OkObjectResult(user);
            }

            return new BadRequestResult();
        }
    }
}
