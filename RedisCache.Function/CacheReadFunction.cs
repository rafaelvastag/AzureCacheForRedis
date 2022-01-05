using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedisCache.Function.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Function
{
    public class CacheReadFunction
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbContext _context;

        public CacheReadFunction(IDistributedCache cache, ApplicationDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        [FunctionName("GetRedisCache")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Category> categoryList = new List<Category>();
            var cachedCategory = _cache.GetString("categoryList");

            if (!string.IsNullOrEmpty(cachedCategory))
            {
                categoryList = JsonConvert.DeserializeObject<List<Category>>(cachedCategory);
            }
            else
            {
                categoryList = await _context.Category.ToListAsync();


                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));
                _cache.SetString("categoryList", JsonConvert.SerializeObject(categoryList), options);
            }


            return new OkObjectResult(categoryList);
        }
    }
}
