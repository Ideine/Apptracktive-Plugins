using System.Net.Http;
using System.Reflection;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// AptkAms plugin configuration
    /// </summary>
    public interface IAptkAmsPluginConfiguration
    {
        /// <summary>
        /// Azure Mobile Service application URL
        /// </summary>
        string AmsAppUrl { get; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        string AmsAppKey { get; }

        /// <summary>
        /// Assembly hosting model classes (usually the same)
        /// </summary>
        Assembly ModelAssembly { get; }

        /// <summary>
        /// [Optional] Credential cache service to manage credentials storing on device
        /// </summary>
        IAptkAmsCredentialsCacheService CredentialsCacheService { get; set; }

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
