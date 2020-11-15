namespace SalesWorkforce.MobileApp.Managers.Entities
{
    public class PurchaseOrderProductEntity 
    {
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double BasePrice { get; set; }
        public double Amount { get; set; }
    }
}
