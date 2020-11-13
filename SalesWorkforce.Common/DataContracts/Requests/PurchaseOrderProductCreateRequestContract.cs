using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class PurchaseOrderProductCreateRequestContract
    {
        public long ProductRecordId { get; set; }
        public double Quantity { get; set; }
    }
}
