using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedisCache.Data;
using RedisCache.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IDistributedCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = new();
            var cachedCategory = _cache.GetString("categoryList");

            if (!string.IsNullOrEmpty(cachedCategory))
            {
                categoryList = JsonConvert.DeserializeObject<List<Category>>(cachedCategory);
            }
            else
            {
                categoryList = _context.Category.ToList();
                DistributedCacheEntryOptions options = new();
                options.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));
                _cache.SetString("categoryList", JsonConvert.SerializeObject(categoryList), options);
            }

            return View(categoryList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
