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
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class PurchaseOrderController : AuthorizeMobileControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly INotificationService _notificationService;

        public PurchaseOrderController(ISalesAgentService salesAgentService,
            IAccessTokenProvider accessTokenProvider,
            IPurchaseOrderService purchaseOrderService,
            INotificationService notificationService) : base(salesAgentService, accessTokenProvider)
        {
            _purchaseOrderService = purchaseOrderService;
            _notificationService = notificationService;
        }

        [FunctionName("PurchaseOrderGetAll")]
        public IActionResult GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "purchase-order/get-all")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            var purchaseOrders = _purchaseOrderService.GetPurchaseOrders(user.RecordId);
            return new OkObjectResult(purchaseOrders);
        }

        [FunctionName("PurchaseOrderGet")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "purchase-order")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            var query = req.Query["recordId"].ToString();

            if (!string.IsNullOrEmpty(query))
            {
                var recordId = Convert.ToInt64(query);
                var purchaseOrder = _purchaseOrderService.GetPurchaseOrder(recordId, user.RecordId);

                if (purchaseOrder != null)
                {
                    purchaseOrder.OrderedProducts = _purchaseOrderService.GetPurchaseOrderProducts(purchaseOrder.RecordId);

                    return new OkObjectResult(purchaseOrder);
                }
            }

            return new BadRequestResult();
        }

        [FunctionName("PurchaseOrderCreate")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "purchase-order/create")] HttpRequest req)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);

            using (var streamReader = new StreamReader(req.Body))
            {
                string requestBody = await streamReader.ReadToEndAsync();
                var contract = JsonConvert.DeserializeObject<PurchaseOrderCreateRequestContract>(requestBody);

                var id = _purchaseOrderService.AddPurchaseOrder(user.RecordId, contract);

                if (id.HasValue)
                {
                    _purchaseOrderService.AddPurchaseOrderProducts(id.Value, contract.Products);

                    var purchaseOrder = _purchaseOrderService.GetPurchaseOrder(id.Value, user.RecordId);

                    if (purchaseOrder != null)
                    {
                        purchaseOrder.OrderedProducts = _purchaseOrderService.GetPurchaseOrderProducts(purchaseOrder.RecordId);

                        return new OkObjectResult(purchaseOrder);
                    }
                }

                return new BadRequestObjectResult(new BadRequestResponseContract() { Message = "Purchase order was not created." });
            }
        }

        [FunctionName("PurchaseOrderNotify")]
        public async Task<IActionResult> Notify([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "purchase-order/notify")] HttpRequest req, ILogger logger)
        {
            using (var streamReader = new StreamReader(req.Body))
            {
                string requestBody = await streamReader.ReadToEndAsync();
                var contract = JsonConvert.DeserializeObject<PurchaseOrderStatusNotificationRequestContract>(requestBody);

                string message = $"Your Purchase Order ({contract.PurchaseOrderNo}) status has been updated to {contract.Status}.";
                _notificationService.SendNotification("Status Updated", message, new List<string>() { contract.SalesAgentId.ToString() }, logger);

                return new OkResult();
            }
        }
    }
}
