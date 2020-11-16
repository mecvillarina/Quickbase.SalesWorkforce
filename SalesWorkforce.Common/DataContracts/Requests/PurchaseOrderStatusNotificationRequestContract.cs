using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class PurchaseOrderStatusNotificationRequestContract
    {
        public string PurchaseOrderNo { get; set; }
        public long SalesAgentId { get; set; }
        public string Status { get; set; }
    }
}
