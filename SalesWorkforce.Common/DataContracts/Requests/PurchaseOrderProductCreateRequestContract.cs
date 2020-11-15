using SalesWorkforce.Common.Abstractions;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class PurchaseOrderProductCreateRequestContract : IJsonDataContract
    {
        public long ProductRecordId { get; set; }
        public double Quantity { get; set; }
    }
}
