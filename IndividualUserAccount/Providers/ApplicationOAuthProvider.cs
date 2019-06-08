using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using IndividualUserAccount.Models;
using System.Diagnostics;

namespace IndividualUserAccount.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (CONS) **********");
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }


        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (ValidateClientAuthentication) **********");
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (GrantResourceOwnerCredentials) **********");
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

      
        
        // FOR Validating client with GOOGLE and OHER SOCIAL MEDIA SERVERS
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (ValidateClientRedirectUri) **********");
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/html/Login.html");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    Debug.WriteLine("*********** inside ---ApplicationOAuthProvider" + expectedRootUri.AbsoluteUri);
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }




        //4th
        public static AuthenticationProperties CreateProperties(string userName)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (CreateProperties) **********");

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }


        //5th
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            Debug.WriteLine("************ApplicationOAuthprovider (TokenEndpoint) **********");
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}