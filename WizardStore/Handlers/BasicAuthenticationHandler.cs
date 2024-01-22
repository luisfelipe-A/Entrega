using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WizardStoreAPI.Data;

namespace WizardStoreAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly WizardStoreContext _DBContext;
        
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, WizardStoreContext dBContext):base(option,logger,encoder,clock)
        {
            _DBContext = dBContext;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No Authorization Header Found");

            var _headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if(!string.IsNullOrEmpty(_headervalue.Parameter))
            {
                var bytes = Convert.FromBase64String(_headervalue.Parameter);
                string credentials = Encoding.UTF8.GetString(bytes);
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];
                
                var user = await this._DBContext.Users
                .FirstOrDefaultAsync(u=>u.UserId.Equals(username) && u.Password.Equals(password));
                if(user==null || user.UserId.IsNullOrEmpty() || user.Password.IsNullOrEmpty())
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }

                var claim = new[]{new Claim(ClaimTypes.Name,username), new Claim(ClaimTypes.Role,"Admin")};
                var identity = new ClaimsIdentity(claim,Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal,Scheme.Name);
                return await Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else return AuthenticateResult.Fail("Unauthorized");
        }
    }
}