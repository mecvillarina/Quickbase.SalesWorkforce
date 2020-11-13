using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.Models
{
    public class QueryResponseModel
    {
        [JsonProperty("data")]
        public List<Dictionary<string, QueryResponseDatum>> Data { get; set; } = new List<Dictionary<string, QueryResponseDatum>>();

        [JsonProperty("fields")]
        public List<QueryResponseField> Fields { get; set; } = new List<QueryResponseField>();

        [JsonProperty("metadata")]
        public QueryResponseMetadata Metadata { get; set; } = new QueryResponseMetadata();
    }

    public class QueryResponseDatum
    {
        [JsonProperty("value")]
        public object Value { get; set; }
    }

    public class QueryResponseField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class QueryResponseMetadata
    {
        [JsonProperty("numFields")]
        public long NumFields { get; set; }

        [JsonProperty("numRecords")]
        public long NumRecords { get; set; }

        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("totalRecords")]
        public long TotalRecords { get; set; }
    }
}
