using System;
using System.Threading.Tasks;
using JwtToken.Function.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace JwtToken.Function
{
    /// <summary>
    ///     Service class for performing authentication.
    /// </summary>
    public class AuthenticationService
    {
        private readonly TokenIssuer _tokenIssuer;

        /// <summary>
        ///     Injection constructor.
        /// </summary>
        /// <param name="tokenIssuer">DI injected token issuer singleton.</param>
        public AuthenticationService(TokenIssuer tokenIssuer)
        {
            _tokenIssuer = tokenIssuer;
        }

        [FunctionName("Authenticate")]
        public async Task<IActionResult> Authenticate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth")] 
            Credentials credentials,
            ILogger log)
        {
            // Perform custom authentication here
            bool authenticated = credentials?.User.Equals("vastag", StringComparison.InvariantCultureIgnoreCase) ?? false;

            if (!authenticated)
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult(_tokenIssuer.IssueTokenForUser(credentials));
        }

        [FunctionName("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "changepassword")]
            HttpRequest req, // Note: we need the underlying request to get the header
            ILogger log)
        {
            // Check if we have authentication info.
            AuthenticationInfo auth = new AuthenticationInfo(req);

            if (!auth.IsValid)
            {
                return new UnauthorizedResult(); // No authentication info.
            }

            string newPassword = await req.ReadAsStringAsync();

            return new OkObjectResult($"{auth.Username} changed password to {newPassword}");
        }
    }
}
