using System;
using System.Collections.Generic;

namespace SalesWorkforce.MobileApp.Managers.Entities
{
    public class PurchaseOrderEntity 
    {
        public long RecordId { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PurchaseOrderStatus { get; set; }
        public DateTimeOffset DateRequested { get; set; }
        public DateTimeOffset? DateShipped { get; set; }
        public DateTimeOffset? DateDelivered { get; set; }
        public double TotalAmount { get; set; }
        public string SalesAgentName { get; set; }
        public string CustomerName { get; set; }
        public List<PurchaseOrderProductEntity> OrderedProducts { get; set; } = new List<PurchaseOrderProductEntity>();

        public string DateRequestedDisplay => DateRequested.ToLocalTime().ToString("MM-dd-yyyy hh:mm tt");
    }
}
