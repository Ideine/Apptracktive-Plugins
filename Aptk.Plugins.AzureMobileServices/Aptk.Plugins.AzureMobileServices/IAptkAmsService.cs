using Aptk.Plugins.AzureMobileServices.Api;
using Aptk.Plugins.AzureMobileServices.Data;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// Service to request Azure Mobile Services
    /// </summary>
    public interface IAptkAmsService
    {
        /// <summary>
        /// Current plugin configuration
        /// </summary>
        IAptkAmsPluginConfiguration Configuration { get; }

        /// <summary>
        /// Current Mobile Service Client
        /// </summary>
        IMobileServiceClient Client { get; }

        /// <summary>
        /// Service to manage data
        /// </summary>
        IAptkAmsDataService Data { get; }

        /// <summary>
        /// Service to handle user Authentication
        /// </summary>
        IAptkAmsIdentityService Identity { get; }

        /// <summary>
        /// Service to send custom request
        /// </summary>
        IAptkAmsApiService Api { get; }
    }
}
