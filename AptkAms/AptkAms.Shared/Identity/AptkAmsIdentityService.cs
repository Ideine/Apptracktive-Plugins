using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsIdentityService : AptkAmsBaseIdentityService
    {
        private readonly IAptkAmsPluginConfiguration _configuration;

        public AptkAmsIdentityService(IAptkAmsPluginConfiguration configuration, IMobileServiceClient client) : base(configuration, client)
        {
            _configuration = configuration;
        }

        public override async Task<MobileServiceUser> LoginAsync(AptkAmsAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            if (provider == AptkAmsAuthenticationProvider.None || provider == AptkAmsAuthenticationProvider.LoginPassword)
                throw new ArgumentException("AptkAmsAuthenticationProvider must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory");

            var user = await AptkAmsPluginLoader.PlatformIdentityInstance.LoginAsync(provider.ToMobileServiceAuthenticationProvider(), parameters, useSingleSignOnIfAvailable);

            _configuration.CredentialsCacheService?.SaveCredentials(new AptkAmsCredentials(provider, user));

            return user;
        }
    }
}
