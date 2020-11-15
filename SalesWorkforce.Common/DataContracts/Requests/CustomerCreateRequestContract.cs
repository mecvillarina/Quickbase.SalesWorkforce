using SalesWorkforce.Common.Abstractions;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class CustomerCreateRequestContract : IJsonDataContract
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
