using System.Collections.Generic;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Abstractions.Identity;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsPlatformIdentityService : IAptkAmsPlatformIdentityService
    {
        private readonly IMobileServiceClient _client;
        private readonly UIApplication _application;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client, UIApplication application)
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
