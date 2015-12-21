using System.Net.Http;
using System.Reflection;
using Aptk.Plugins.AzureForMobile.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile
{
    /// <summary>
    /// Azure For Mobile Plugin configuration
    /// </summary>
    public class AzureForMobilePluginConfiguration : IAzureForMobilePluginConfiguration
    {
        /// <summary>
        /// Azure For Mobile Plugin configuration constructor
        /// </summary>
        /// <param name="amsUrl">Azure Mobile Services URL</param>
        /// <param name="modelAssembly">Model classes hosting Assembly (usually the same)</param>
        public AzureForMobilePluginConfiguration(string amsUrl, Assembly modelAssembly)
        {
            AmsUrl = amsUrl;
            ModelAssembly = modelAssembly;
        }

        /// <summary>
        /// Azure For Mobile Plugin configuration constructor
        /// </summary>
        /// <param name="amsUrl">Azure Mobile Services URL</param>
        /// <param name="amsKey">Azure Mobile Services Key</param>
        /// <param name="modelAssembly">Model classes hosting Assembly (usually the same)</param>
        public AzureForMobilePluginConfiguration(string amsUrl, string amsKey, Assembly modelAssembly)
        {
            AmsUrl = amsUrl;
            AmsKey = amsKey;
            ModelAssembly = modelAssembly;
        }

        /// <summary>
        /// Azure Mobile Services URL
        /// </summary>
        public string AmsUrl { get; }

        /// <summary>
        /// Azure Mobile Services Key
        /// </summary>
        public string AmsKey { get; }

        /// <summary>
        /// Assembly hosting model classes (usually the same)
        /// </summary>
        public Assembly ModelAssembly { get; }

        /// <summary>
        /// [Optional] Credential cache service to manage credentials storing on device
        /// </summary>
        public IAzureForMobileCredentialsCacheService CredentialsCacheService { get; set; }

        /// <summary>
        /// [Optional] Custom Http message handlers
        /// </summary>
        public HttpMessageHandler[] Handlers { get; set; }

        /// <summary>
        /// [Optional] Json serializer settings
        /// </summary>
        public MobileServiceJsonSerializerSettings SerializerSettings { get; set; }
    }
}
