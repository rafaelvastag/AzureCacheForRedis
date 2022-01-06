using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using ServiceBus.Functions.Models;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace ServiceBus.Functions
{
    public static class ServiceBus
    {
        [return: ServiceBus("checkoutmessagetopic", ServiceBusEntityType.Topic)]
        [FunctionName("ServiceBusExample")]
        public static async Task<ResponseDTO> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var headers = req.Headers;
            log.LogInformation("SendMessage function requested");
            string body = string.Empty;
            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
                log.LogInformation($"Message body : {body}");
            }
            log.LogInformation($"SendMessage processed.");
            var message = new ResponseDTO("Response OK", body,headers["HEADER_VASTAG"]);

            return message;
        }

    }
}
