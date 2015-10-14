using System.Collections.Generic;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    public class AptkAmsPlatformIdentityService : IAptkAmsPlatformIdentityService
    {
        private readonly IMobileServiceClient _client;
        private readonly bool _useSingleSignOn;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client, bool useSingleSignOn)
        {
            _client = client;
            _useSingleSignOn = useSingleSignOn;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            return await _client.LoginAsync(provider, _useSingleSignOn, parameters);
        }
    }
}
