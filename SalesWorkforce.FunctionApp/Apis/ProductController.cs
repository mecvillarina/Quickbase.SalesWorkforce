using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class ProductController : AuthorizeMobileControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(ISalesAgentService salesAgentService,
            IAccessTokenProvider accessTokenProvider,
            IProductService productService) : base(salesAgentService, accessTokenProvider)
        {
            _productService = productService;
        }

        [FunctionName("ProductQuery")]
        public IActionResult Query([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/search")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var query = req.Query["query"].ToString();

            var customers = _productService.GetProducts(query);
            return new OkObjectResult(customers);
        }

        [FunctionName("ProductGet")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")] HttpRequest req, ILogger logger)
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
                var customers = _productService.GetProduct(recordId);
                return new OkObjectResult(customers);
            }

            return new BadRequestResult();
        }

    }
}
