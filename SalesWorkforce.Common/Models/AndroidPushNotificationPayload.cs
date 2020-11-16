using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.Models
{
    public partial class AndroidPushNotificationPayload
    {
        [JsonProperty("data")]
        public AndroidPushNotificationData Data { get; set; } = new AndroidPushNotificationData();
    }

    public partial class AndroidPushNotificationData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; } = "max";

        [JsonProperty("silent")]
        public string Silent { get; set; } = "false";
    }

}
