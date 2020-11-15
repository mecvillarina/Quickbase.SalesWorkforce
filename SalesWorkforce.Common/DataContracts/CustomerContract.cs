using SalesWorkforce.Common.Abstractions;
using SalesWorkforce.Common.Models;
using System.Collections.Generic;

namespace SalesWorkforce.Common.DataContracts
{
    public class CustomerContract : IJsonDataContract
    {
        public long RecordId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }

        public CustomerContract()
        {

        }

        public CustomerContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            Name = data["6"].Value.ToString();
            Address = data["7"].Value.ToString();
            ContactNumber = data["8"].Value.ToString();
            Email = data["9"].Value.ToString();
            BillingAddress = data["10"].Value.ToString();
            DeliveryAddress = data["11"].Value.ToString();
        }
    }
}
