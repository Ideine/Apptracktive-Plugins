    using System;
using System.Collections.Generic;
using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Security;
    using Newtonsoft.Json.Linq;
    using Owin;

namespace AzureForMobile.Sample.Backend.Utils
{
    public class CustomLoginProvider : LoginProvider
    {
        internal const string ProviderName = "custom";

        public override string Name
        {
            get { return ProviderName; }
        }

        public CustomLoginProvider(IServiceTokenHandler tokenHandler)
            : base(tokenHandler)
        {
            TokenLifetime = TimeSpan.FromDays(365);
        }

        public override void ConfigureMiddleware(IAppBuilder appBuilder, ServiceSettingsDictionary settings)
        {
            // Not Applicable - used for federated identity flows
        }

        public override ProviderCredentials CreateCredentials(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            var username = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var credentials = new CustomLoginProviderCredentials
            {
                UserId = TokenHandler.CreateUserId(Name, username)
            };

            return credentials;
        }

        public override ProviderCredentials ParseCredentials(JObject serialized)
        {
            if (serialized == null)
            {
                throw new ArgumentNullException("serialized");
            }

            return serialized.ToObject<CustomLoginProviderCredentials>();
        }
    }
}