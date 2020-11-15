namespace SalesWorkforce.MobileApp.Managers.Entities
{
    public class ProductEntity
    {
        public long RecordId { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public string Unit { get; set; }
    }
}
