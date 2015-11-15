using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsPlatformIdentityService : IAptkAmsPlatformIdentityService
    {
        private readonly IMobileServiceClient _client;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            return await _client.LoginAsync(provider, useSingleSignOnIfAvailable, parameters);
        }
    }
}
