using System.Net.Http;
using System.Reflection;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// AptkAms plugin configuration
    /// </summary>
    public class AptkAmsPluginConfiguration : IAptkAmsPluginConfiguration
    {
        /// <summary>
        /// AptkAms plugin configuration constructor
        /// </summary>
        /// <param name="amsAppUrl">Azure Mobile Service application URL</param>
        /// <param name="amsAppKey">Azure Mobile Service application Key</param>
        /// <param name="modelAssembly">Model classes hosting Assembly (usually the same)</param>
        public AptkAmsPluginConfiguration(string amsAppUrl, string amsAppKey, Assembly modelAssembly)
        {
            AmsAppUrl = amsAppUrl;
            AmsAppKey = amsAppKey;
            ModelAssembly = modelAssembly;
        }

        /// <summary>
        /// Azure Mobile Service application URL
        /// </summary>
        public string AmsAppUrl { get; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        public string AmsAppKey { get; }

        /// <summary>
        /// Assembly hosting model classes (usually the same)
        /// </summary>
        public Assembly ModelAssembly { get; }

        /// <summary>
        /// [Optional] Credential cache service to manage credentials storing on device
        /// </summary>
        public IAptkAmsCredentialsCacheService CredentialsCacheService { get; set; }

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
