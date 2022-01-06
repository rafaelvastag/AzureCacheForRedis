using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


[assembly: FunctionsStartup(typeof(JwtToken.Function.Startup))]
namespace JwtToken.Function
{
    /// <summary>
    ///     Startup class used to initialize the dependency injection.
    /// </summary>
    /// <remarks>
    ///     See: https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
    /// </remarks>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        ///     Configure the DI container.
        /// </summary>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Intject the token service.
            builder.Services.AddSingleton<TokenIssuer>();
        }
    }
}
