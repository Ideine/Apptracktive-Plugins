using Aptk.Plugins.AzureMobileServices.Api;
using Aptk.Plugins.AzureMobileServices.Data;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class AptkAmsService : IAptkAmsService
    {
        public AptkAmsService(IAptkAmsPluginConfiguration configuration, IMobileServiceClient client)
        {
            Configuration = configuration;
            Client = client;
        }

        public IAptkAmsPluginConfiguration Configuration { get; }

        public IMobileServiceClient Client { get; }

        public IAptkAmsDataService Data => AptkAmsPluginLoader.DataInstance;

        public IAptkAmsIdentityService Identity => AptkAmsPluginLoader.IdentityInstance;

        public IAptkAmsApiService Api => AptkAmsPluginLoader.ApiInstance;
    }
}