using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public class AzureForMobileIdentityService : AzureForMobileBaseIdentityService
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;

        public AzureForMobileIdentityService(IAzureForMobilePluginConfiguration configuration, IMobileServiceClient client) : base(configuration, client)
        {
            _configuration = configuration;
        }

        public override async Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            if (provider == AzureForMobileAuthenticationProvider.None || provider == AzureForMobileAuthenticationProvider.Custom)
                throw new ArgumentException("AzureForMobileAuthenticationProvider must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory");

            var user = await AzureForMobilePluginLoader.PlatformIdentityInstance.LoginAsync(provider.ToMobileServiceAuthenticationProvider(), parameters, useSingleSignOnIfAvailable);

            _configuration.CredentialsCacheService?.SaveCredentials(new AzureForMobileCredentials(provider, user));

            return user;
        }
    }
}
