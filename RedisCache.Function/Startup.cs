using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RedisCache.Function.Startup))]
namespace RedisCache.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("SQLConnectionAzurePoC");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => SqlServerDbContextOptionsExtensions
                    .UseSqlServer(options, connectionString));

            builder.Services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("AzureRedisConnection");
            });
        }

    }
}
