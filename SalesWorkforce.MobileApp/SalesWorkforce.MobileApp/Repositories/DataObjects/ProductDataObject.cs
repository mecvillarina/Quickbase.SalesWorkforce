using SalesWorkforce.MobileApp.Repositories.Base;

namespace SalesWorkforce.MobileApp.Repositories.DataObjects
{
    public class ProductDataObject : DataObjectBase
    {
        public long RecordId { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public string Unit { get; set; }
    }
}
