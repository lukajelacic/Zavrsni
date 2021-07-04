using eMusicStudio.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IKorisniciService _userService;
        private readonly IKlijentiService _klijentService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IKorisniciService userService, IKlijentiService klijentiService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
            _klijentService = klijentiService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            Model.Korisnici user = null;
            Model.Klijenti klijent = null;
            var context = "";
            try
            {
                context = Request.Headers["Context"];
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                if ("Klijenti".Equals(context))
                {
                    klijent = _klijentService.Authenticiraj(username, password);
                    var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, klijent.KorisnickoIme);

                }
                else
                {
                    user = _userService.Authenticiraj(username, password);
                    var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, user.KorisnickoIme);

                }

            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            var claims = new List<Claim>();
            if ("Klijenti".Equals(context))
            {
                if (klijent == null)
                    return AuthenticateResult.Fail("Invalid Username or Password");
            }
            else
            {
                if (user == null)
                    return AuthenticateResult.Fail("Invalid Username or Password");
                claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.KorisnickoIme),
                new Claim(ClaimTypes.Name, user.Ime),
                new Claim(ClaimTypes.Role, user.UlogaId.ToString())
            };
            }
            //if (user == null)
            //    return AuthenticateResult.Fail("Invalid Username or Password");

            //var claims = new List<Claim> {
            //    new Claim(ClaimTypes.NameIdentifier, user.KorisnickoIme),
            //    new Claim(ClaimTypes.Name, user.Ime),
            //    new Claim(ClaimTypes.Role, user.UlogaId.ToString())
            //};


            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
    //public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    //{
    //    public BasicAuthenticationHandler(
    //        IOptionsMonitor<AuthenticationSchemeOptions> options,
    //        ILoggerFactory logger,
    //        UrlEncoder encoder,
    //        ISystemClock clock
    //        )
    //: base(options, logger, encoder, clock)
    //    {
    //    }

    //    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        Response.Headers.Add("WWW-Authenticate", "Basic");

    //        if (!Request.Headers.ContainsKey("Authorization"))
    //        {
    //            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
    //        }

    //        // Get authorization key
    //        var authorizationHeader = Request.Headers["Authorization"].ToString();
    //        var authHeaderRegex = new Regex(@"Basic (.*)");

    //        if (!authHeaderRegex.IsMatch(authorizationHeader))
    //        {
    //            return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
    //        }

    //        var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
    //        var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
    //        var authUsername = authSplit[0];
    //        var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");

    //        if (authUsername != "roundthecode" || authPassword != "roundthecode")
    //        {
    //            return Task.FromResult(AuthenticateResult.Fail("The username or password is not correct."));
    //        }

    //        var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, "roundthecode");
    //        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

    //        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    //    }
    //}
}