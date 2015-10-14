using System.Collections.Generic;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace Aptk.Plugins.AzureMobileServices
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

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            return await _client.LoginAsync(_controller, provider, parameters);
        }
    }
}
