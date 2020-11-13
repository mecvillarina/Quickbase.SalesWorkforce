using SalesWorkforce.Common.Models;
using System;
using System.Collections.Generic;

namespace SalesWorkforce.Common.DataContracts
{
    public class PurchaseOrderProductContract
    {
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double BasePrice { get; set; }
        public double Amount { get; set; }

        public PurchaseOrderProductContract()
        {

        }

        public PurchaseOrderProductContract(Dictionary<string, Datum> data)
        {
            ProductSKU = data["12"].Value.ToString();
            ProductName = data["13"].Value.ToString();

            if (data["6"].Value != null)
            {
                Quantity = Convert.ToDouble(data["6"].Value);
            }

            if (data["7"].Value != null)
            {
                Amount = Convert.ToDouble(data["7"].Value);
            }

            if (data["14"].Value != null)
            {
                BasePrice = Convert.ToDouble(data["14"].Value);
            }
        }
    }
}
