using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.Models
{
    public class Datum
    {
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
