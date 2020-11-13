using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class CustomerController : AuthorizeMobileControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(IAccessTokenProvider accessTokenProvider, ICustomerService customerService) : base(accessTokenProvider)
        {
            _customerService = customerService;
        }

        [FunctionName("CustomerQuery")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/search")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var query = req.Query["query"].ToString();

            var customers = _customerService.GetCustomers(query);
            return new OkObjectResult(customers);
        }
    }
}
