using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.Common.DataContracts.Responses;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class CustomerController : AuthorizeMobileControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ISalesAgentService salesAgentService, IAccessTokenProvider accessTokenProvider, ICustomerService customerService) : base(salesAgentService, accessTokenProvider)
        {
            _customerService = customerService;
        }

        [FunctionName("CustomerQuery")]
        public IActionResult Query([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/search")] HttpRequest req, ILogger logger)
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

        [FunctionName("CustomerGet")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var query = req.Query["recordId"].ToString();

            if (!string.IsNullOrEmpty(query))
            {
                var recordId = Convert.ToInt64(query);
                var customers = _customerService.GetCustomer(recordId);
                return new OkObjectResult(customers);
            }

            return new BadRequestResult();
        }

        [FunctionName("CustomerCreate")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customer/create")] HttpRequest req)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            using (var streamReader = new StreamReader(req.Body))
            {
                string requestBody = await streamReader.ReadToEndAsync();
                var contract = JsonConvert.DeserializeObject<CustomerCreateRequestContract>(requestBody);

                var id = _customerService.AddCustomer(contract);

                if (id.HasValue)
                {
                    var customer = _customerService.GetCustomer(id.Value);
                    return new OkObjectResult(customer);
                }

                return new BadRequestObjectResult(new BadRequestResponseContract() { Message = "Customer was not created" });
            }
        }
    }
}
