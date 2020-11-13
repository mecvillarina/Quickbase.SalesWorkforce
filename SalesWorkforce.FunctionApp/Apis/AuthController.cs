using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class AuthController : AuthorizeMobileControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAccessTokenProvider accessTokenProvider, IAuthService authService) : base(accessTokenProvider)
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
    }
}
