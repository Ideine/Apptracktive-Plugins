using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    public class AptkAmsPlatformIdentityService : IAptkAmsPlatformIdentityService
    {
        private readonly IMobileServiceClient _client;
        private readonly Context _context;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client, Context context)
        {
            _client = client;
            _context = context;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            return await _client.LoginAsync(_context, provider, parameters);
        }
    }
}