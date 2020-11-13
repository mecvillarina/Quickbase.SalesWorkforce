using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface IPurchaseOrderService
    {
        PurchaseOrderContract GetPurchaseOrder(long recordId, long salesAgentId);
        List<PurchaseOrderContract> GetPurchaseOrders(long salesAgentId);

        long? AddPurchaseOrder(long salesAgentId, PurchaseOrderCreateRequestContract reqContract);
        void AddPurchaseOrderProducts(long purchaseOrderId, List<PurchaseOrderProductCreateRequestContract> productsReqContract);
        List<PurchaseOrderProductContract> GetPurchaseOrderProducts(long purchaseOrderRecordId);
    }
}