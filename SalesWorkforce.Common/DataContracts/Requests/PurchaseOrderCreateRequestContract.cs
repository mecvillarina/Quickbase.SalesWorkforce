using SalesWorkforce.Common.Abstractions;
using System.Collections.Generic;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class PurchaseOrderCreateRequestContract : IJsonDataContract
    {
        public long CustomerRecordId { get; set; }

        public List<PurchaseOrderProductCreateRequestContract> Products { get; set; } = new List<PurchaseOrderProductCreateRequestContract>();
    }
}
