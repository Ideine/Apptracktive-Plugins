using System.Net.Http;
using System.Reflection;
using Aptk.Plugins.AzureForMobile.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile
{
    /// <summary>
    /// AzureForMobile plugin configuration
    /// </summary>
    public interface IAzureForMobilePluginConfiguration
    {
        /// <summary>
        /// Azure Mobile Services URL
        /// </summary>
        string AmsUrl { get; }

        /// <summary>
        /// Azure Mobile Services Key
        /// </summary>
        string AmsKey { get; }

        /// <summary>
        /// Assembly hosting model classes (usually the same)
        /// </summary>
        Assembly ModelAssembly { get; }

        /// <summary>
        /// [Optional] Credential cache service to manage credentials storing on device
        /// </summary>
        IAzureForMobileCredentialsCacheService CredentialsCacheService { get; set; }

        /// <summary>
        /// [Optional] Custom Http message handlers
        /// </summary>
        HttpMessageHandler[] Handlers { get; set; }

        /// <summary>
        /// [Optional] Json serializer settings
        /// </summary>
        MobileServiceJsonSerializerSettings SerializerSettings { get; set; }
    }
}
