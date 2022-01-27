using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureCosmosDB.Model
{
    public class Poc
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("instance")]
        public string ServiceName { get; set; }
        [JsonProperty("size")]
        public decimal ServiceSize { get; set; }
        [JsonProperty("date")]
        public DateTime CreateAt { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
    }
}

