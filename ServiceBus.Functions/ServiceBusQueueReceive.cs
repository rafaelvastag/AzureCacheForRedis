using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ServiceBus.Functions.Models;

namespace ServiceBus.Functions
{
    public static class ServiceBusQueueReceive
    {
        [FunctionName("ServiceBusQueueReceive")]
        public static void Run([ServiceBusTrigger("checkoutmessagetopic", "vastagOrderSubscription", Connection = "AzureWebJobsServiceBus")] ServiceBusReceivedMessage mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed TOKEN: {mySbMsg.ApplicationProperties["token"]}");
        }
    }
}
