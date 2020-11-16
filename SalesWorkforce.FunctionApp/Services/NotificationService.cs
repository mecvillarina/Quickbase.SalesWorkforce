using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationHubClient _hub;

        public NotificationService(IConfiguration configuration)
        {
            string hubName = configuration["AzurePNHubName"];
            string hubConnectionString = configuration["AzurePNHubConnectionString"];
            _hub = new NotificationHubClient(hubConnectionString, hubName);
        }

        public void SendNotification(string title, string message, List<string> tags, ILogger logger)
        {
            SendAndroid(title, message, tags, logger);
            SendApple(title, message, tags);
        }

        private void SendAndroid(string title, string message, List<string> tags, ILogger logger)
        {
            try
            {
                var random = new Random(DateTime.Now.Millisecond);

                var androidPayload = new AndroidPushNotificationPayload();
                androidPayload.Data = new AndroidPushNotificationData()
                {
                    Title = title,
                    Body = message,
                    Id = random.Next(1, 9999),
                };

                string androidPayloadStr = JsonConvert.SerializeObject(androidPayload);
                _hub.SendFcmNativeNotificationAsync(androidPayloadStr, tags).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                //Do Nothing
            }
        }

        private void SendApple(string title, string message, List<string> tags)
        {
            try
            {
                var applePayload = new ApplePushNotificationPayload();
                applePayload.Aps = new ApplePushNotificationData()
                {
                    Alert = new ApplePushNotificationAlert()
                    {
                        Body = message,
                        Title = title
                    }
                };

                string payloadStr = JsonConvert.SerializeObject(applePayload);
                 _hub.SendAppleNativeNotificationAsync(payloadStr, tags).Wait();
            }
            catch (Exception ex)
            {
                //Do Nothing
            }
        }
    }
}
