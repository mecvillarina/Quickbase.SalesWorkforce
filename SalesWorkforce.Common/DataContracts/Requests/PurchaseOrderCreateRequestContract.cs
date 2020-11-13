using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class PurchaseOrderCreateRequestContract
    {
        public long CustomerRecordId { get; set; }

        public List<PurchaseOrderProductCreateRequestContract> Products { get; set; } = new List<PurchaseOrderProductCreateRequestContract>();
    }
}
