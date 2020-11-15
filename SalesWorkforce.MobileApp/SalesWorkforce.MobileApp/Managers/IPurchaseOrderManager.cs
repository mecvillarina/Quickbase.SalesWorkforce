using SalesWorkforce.MobileApp.Managers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Managers
{
    public interface IPurchaseOrderManager
    {
        Task<List<PurchaseOrderEntity>> GetPurchaseOrders();
        Task<PurchaseOrderEntity> GetPurchaseOrder(long recordId);
        Task<PurchaseOrderEntity> Create(PurchaseOrderCreateRequestEntity reqEntity);
    }
}