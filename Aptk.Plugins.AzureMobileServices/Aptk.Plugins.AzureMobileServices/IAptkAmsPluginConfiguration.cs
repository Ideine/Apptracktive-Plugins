using System.Reflection;

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
        string AmsAppUrl { get; set; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        string AmsAppKey { get; set; }

        /// <summary>
        /// Assembly hosting model classes (usually Core)
        /// </summary>
        Assembly ModelAssembly { get; set; }

        /// <summary>
        /// Use single sign on if available (depending of the platform)
        /// </summary>
        bool UseSingleSignOnIfAvailable { get; set; }
    }
}
