using System.Collections.Generic;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Abstractions;
using Aptk.Plugins.AzureMobileServices.Abstractions.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsIdentityService : AptkAmsBaseIdentityService
    {
        public AptkAmsIdentityService(IAptkAmsPluginConfiguration configuration, IMobileServiceClient client) : base(configuration, client)
        { }

        public override async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false)
        {
            return await Loader.PlatformIdentityInstance.LoginAsync(provider, parameters, useSingleSignOnIfAvailable);
        }
    }
}
