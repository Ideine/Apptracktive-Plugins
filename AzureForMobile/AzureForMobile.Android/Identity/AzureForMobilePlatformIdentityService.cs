using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public class AzureForMobilePlatformIdentityService : IAzureForMobilePlatformIdentityService
    {
        private readonly IMobileServiceClient _client;
        private readonly Context _context;

        public AzureForMobilePlatformIdentityService(IMobileServiceClient client, Context context)
        {
            _client = client;
            _context = context;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            return await _client.LoginAsync(_context, provider, parameters);
        }
    }
}