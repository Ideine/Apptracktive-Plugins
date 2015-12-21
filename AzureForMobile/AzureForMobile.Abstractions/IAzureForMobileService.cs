
using Aptk.Plugins.AzureForMobile.Api;
using Aptk.Plugins.AzureForMobile.Data;
using Aptk.Plugins.AzureForMobile.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile
{
    /// <summary>
    /// Azure For Mobile main plugin service
    /// </summary>
    public interface IAzureForMobileService
    {
        /// <summary>
        /// Current plugin configuration
        /// </summary>
        IAzureForMobilePluginConfiguration Configuration { get; }

        /// <summary>
        /// Current Mobile Service Client
        /// </summary>
        IMobileServiceClient Client { get; }

        /// <summary>
        /// Service to manage data
        /// </summary>
        IAzureForMobileDataService Data { get; }

        /// <summary>
        /// Service to handle user Authentication
        /// </summary>
        IAzureForMobileIdentityService Identity { get; }

        /// <summary>
        /// Service to send custom request
        /// </summary>
        IAzureForMobileApiService Api { get; }
    }
}
