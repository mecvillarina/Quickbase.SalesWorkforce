using Microsoft.Extensions.Options;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWorkforce.FunctionApp.Services
{
    public class PurchaseOrderService : ServiceBase, IPurchaseOrderService
    {
        private readonly string _dbId = "bqzak73hr";
        public PurchaseOrderService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public long? AddPurchaseOrder(long salesAgentId, PurchaseOrderCreateRequestContract reqContract)
        {
            var row = new Dictionary<string, Datum>();
            row.Add("11", new Datum() { Value = salesAgentId });
            row.Add("14", new Datum() { Value = reqContract.CustomerRecordId });

            var postRequest = new PostRequestModel(_dbId);
            postRequest.FieldsToReturn = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 18 };
            postRequest.Data.Add(row);

            var result = PostRequest<PostRequestModel, PostResponseModel>("/v1/records", postRequest);

            if (result.Data.Any())
            {
                return (long)result.Data.First()["3"].Value;
            }

            return null;
        }

        public void AddPurchaseOrderProducts(long purchaseOrderId, List<PurchaseOrderProductCreateRequestContract> productsReqContract)
        {
            var postRequest = new PostRequestModel("bqzam4xym");
            postRequest.FieldsToReturn = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            foreach (var productReq in productsReqContract)
            {
                var row = new Dictionary<string, Datum>();
                row.Add("6", new Datum() { Value = productReq.Quantity });
                row.Add("8", new Datum() { Value = purchaseOrderId });
                row.Add("11", new Datum() { Value = productReq.ProductRecordId });
                postRequest.Data.Add(row);
            }

            PostRequest<PostRequestModel, PostResponseModel>("/v1/records", postRequest);
        }

        public List<PurchaseOrderContract> GetPurchaseOrders(long salesAgentId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 18 };
            queryRequest.Where = $"{{11.EX.{salesAgentId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var purchaseOrders = new List<PurchaseOrderContract>();

            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    purchaseOrders.Add(new PurchaseOrderContract(data));
                }

            }

            return purchaseOrders;
        }

        public PurchaseOrderContract GetPurchaseOrder(long recordId, long salesAgentId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 18 };
            queryRequest.Where = $"{{3.EX.{recordId}}}AND{{11.EX.{salesAgentId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            if (result.Data != null)
            {
                var data = result.Data.First();
                return new PurchaseOrderContract(data);
            }

            return null;
        }

        public List<PurchaseOrderProductContract> GetPurchaseOrderProducts(long purchaseOrderRecordId)
        {
            var queryRequest = new QueryRequestModel("bqzam4xym");
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            queryRequest.Where = $"{{8.EX.{purchaseOrderRecordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var purchaseOrders = new List<PurchaseOrderProductContract>();

            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    purchaseOrders.Add(new PurchaseOrderProductContract(data));
                }

            }

            return purchaseOrders;
        }
    }
}
