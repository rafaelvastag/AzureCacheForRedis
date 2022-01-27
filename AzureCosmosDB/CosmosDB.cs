using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureCosmosDB.Model;
using MongoDB.Bson;

namespace AzureCosmosDB
{
    public static class CosmosDB
    {
        [FunctionName("CosmosDbWrite")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cosmos-db")] HttpRequest req, [CosmosDB(
                databaseName: "POC_COSMOS_DB",
                containerName: "Poc",
                Connection = "CosmosDBConnection")]out Poc pocDocument,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var pocRequest = JsonConvert.DeserializeObject<Poc>(requestBody);

            var poc = new Poc
            {
                Id = Guid.NewGuid().ToString(),
                ServiceName = pocRequest.ServiceName,
                CreateAt = DateTime.Now,
                ServiceSize = pocRequest.ServiceSize,
                Author = pocRequest.Author
            };

            pocDocument = poc;

        }

        [FunctionName(nameof(InsertProductTrigger))]
        public static IActionResult InsertProductTrigger(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertBooksTrigger")] HttpRequest req,
           [CosmosDB(
                databaseName: "BookstoreDB",
                containerName: "Books",
                Connection = "CosmosDBConnection")]out Book bookDocument,
           ILogger log)
        {

            var incomingRequest = new StreamReader(req.Body).ReadToEnd();

            var bookRequest = JsonConvert.DeserializeObject<Book>(incomingRequest);
            bookRequest.BookName = bookRequest.BookName.ToUpperInvariant();

            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                BookName = bookRequest.BookName,
                Price = bookRequest.Price,
                Category = bookRequest.Category,
                Author = bookRequest.Author
            };

            bookDocument = book;

            return new StatusCodeResult(StatusCodes.Status201Created);

        }
    }
}
