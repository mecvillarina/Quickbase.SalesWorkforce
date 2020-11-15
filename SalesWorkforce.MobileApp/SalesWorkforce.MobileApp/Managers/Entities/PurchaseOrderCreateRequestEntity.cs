using System.Collections.Generic;

namespace SalesWorkforce.MobileApp.Managers.Entities
{
    public class PurchaseOrderCreateRequestEntity
    {
        public long CustomerRecordId { get; set; }

        public List<PurchaseOrderProductCreateRequestEntity> Products { get; set; } = new List<PurchaseOrderProductCreateRequestEntity>();
    }
}
