using Aptk.Plugins.AzureForMobile.Api;
using Aptk.Plugins.AzureForMobile.Data;
using Aptk.Plugins.AzureForMobile.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class AzureForMobileService : IAzureForMobileService
    {
        public AzureForMobileService(IAzureForMobilePluginConfiguration configuration, IMobileServiceClient client)
        {
            Configuration = configuration;
            Client = client;
        }

        public IAzureForMobilePluginConfiguration Configuration { get; }

        public IMobileServiceClient Client { get; }

        public IAzureForMobileDataService Data => AzureForMobilePluginLoader.DataInstance;

        public IAzureForMobileIdentityService Identity => AzureForMobilePluginLoader.IdentityInstance;

        public IAzureForMobileApiService Api => AzureForMobilePluginLoader.ApiInstance;
    }
}