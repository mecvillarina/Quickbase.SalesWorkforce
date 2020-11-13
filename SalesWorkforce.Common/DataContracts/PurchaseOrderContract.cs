using SalesWorkforce.Common.Models;
using System;
using System.Collections.Generic;

namespace SalesWorkforce.Common.DataContracts
{
    public class PurchaseOrderContract
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
        public List<PurchaseOrderProductContract> OrderedProducts { get; set; } = new List<PurchaseOrderProductContract>();

        public PurchaseOrderContract()
        {

        }

        public PurchaseOrderContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            PurchaseOrderNo = data["6"].Value.ToString();
            PurchaseOrderStatus = data["7"].Value.ToString();
            DateRequested = DateTimeOffset.Parse(data["8"].Value.ToString());

            if (!string.IsNullOrEmpty(data["9"].Value.ToString()))
            {
                DateShipped = DateTimeOffset.Parse(data["9"].Value.ToString());
            }

            if (!string.IsNullOrEmpty(data["10"].Value.ToString()))
            {
                DateDelivered = DateTimeOffset.Parse(data["10"].Value.ToString());
            }

            SalesAgentName = data["12"].Value.ToString();
            CustomerName = data["15"].Value.ToString();

            if (data["18"].Value != null)
            {
                TotalAmount = Convert.ToDouble(data["18"].Value);
            }
        }

    }
}
