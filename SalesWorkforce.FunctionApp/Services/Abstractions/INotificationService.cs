using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface INotificationService
    {
        void SendNotification(string title, string message, List<string> tags, ILogger logger);
    }
}