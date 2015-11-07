using Aptk.Plugins.AzureMobileServices.Abstractions.Api;
using Aptk.Plugins.AzureMobileServices.Abstractions.Data;
using Aptk.Plugins.AzureMobileServices.Abstractions.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Abstractions
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