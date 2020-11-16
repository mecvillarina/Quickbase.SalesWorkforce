using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.Models
{
    public class ApplePushNotificationPayload
    {
        [JsonProperty("aps")]
        public ApplePushNotificationData Aps { get; set; }
    }

    public class ApplePushNotificationData
    {
        [JsonProperty("alert")]
        public ApplePushNotificationAlert Alert { get; set; }
    }

    public class ApplePushNotificationAlert
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }

}
