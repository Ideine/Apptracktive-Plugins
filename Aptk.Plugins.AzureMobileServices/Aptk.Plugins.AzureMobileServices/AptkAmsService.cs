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
        public AptkAmsService(IAptkAmsPluginConfiguration configuration, IMobileServiceClient client, IAptkAmsDataService data, IAptkAmsIdentityService identity, IAptkAmsApiService api)
        {
            Configuration = configuration;
            Client = client;
            Data = data;
            Identity = identity;
            Api = api;
        }

        public IAptkAmsPluginConfiguration Configuration { get; }

        public IMobileServiceClient Client { get; }

        public IAptkAmsDataService Data { get; }

        public IAptkAmsIdentityService Identity { get; }

        public IAptkAmsApiService Api { get; }
    }
}