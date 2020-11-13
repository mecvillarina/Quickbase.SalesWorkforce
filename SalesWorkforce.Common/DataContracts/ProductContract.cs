using SalesWorkforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts
{
    public class ProductContract
    {
        public long RecordId { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public string Unit { get; set; }
        public List<ProductImageContract> Images { get; set; } = new List<ProductImageContract>();

        public ProductContract()
        {

        }

        public ProductContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            SKU = data["6"].Value.ToString();
            Name = data["7"].Value.ToString();
            Description = data["8"].Value.ToString();
            BasePrice = Convert.ToDouble(data["9"].Value);
            Unit = data["11"].Value.ToString();
        }
    }
}
