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
        private readonly UIViewController _controller;

        public AptkAmsPlatformIdentityService(IMobileServiceClient client, UIViewController controller)
        {
            _client = client;
            _controller = controller;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            return await _client.LoginAsync(_controller, provider, parameters);
        }
    }
}
