using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public class AzureForMobilePlatformIdentityService : IAzureForMobilePlatformIdentityService
    {
        private readonly IMobileServiceClient _client;
        private readonly UIApplication _application;

        public AzureForMobilePlatformIdentityService(IMobileServiceClient client, UIApplication application)
        {
            _client = client;
            _application = application;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            return await _client.LoginAsync(_application.KeyWindow.RootViewController, provider, parameters);
        }
    }
}
