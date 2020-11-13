using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts
{
    public class CustomerContract
    {
        public long RecordId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
