using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;

namespace WeatherApp.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("No Authentication Header"));

            try
            {
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization.ToString());

                if (authenticationHeader.Scheme != "Basic")
                {
                    return Task.FromResult(AuthenticateResult.Fail("Incorrect Authorization Scheme. Basic Scheme Required"));
                }

                var authenticationHeaderParameter = authenticationHeader.Parameter;
                if (authenticationHeaderParameter is null)
                    return Task.FromResult(AuthenticateResult.Fail("Missing Credentials"));

                var authTokens = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeaderParameter));
            }
            catch (FormatException ex)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Incorrect Authorization Header Format: {ex}"));
            }
            
            throw new NotImplementedException();
        }
    }
}
