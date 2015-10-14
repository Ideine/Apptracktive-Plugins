using System.Collections.Generic;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    public class AptkAmsPlatformIdentityService : IAptkAmsPlatformIdentityService
    {
        private readonly IMobileServiceClient _client;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            return await _client.LoginAsync(provider, parameters);
        }
    }
}
