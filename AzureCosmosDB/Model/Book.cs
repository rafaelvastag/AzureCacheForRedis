using Newtonsoft.Json;

namespace AzureCosmosDB.Model
{

    public class Book
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string BookName { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
    }
}
