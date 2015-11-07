using System.Reflection;

namespace Aptk.Plugins.AzureMobileServices.Abstractions
{
    /// <summary>
    /// MvxAms plugin base configuration
    /// </summary>
    public abstract class AptkAmsPluginBaseConfiguration : IAptkAmsPluginConfiguration
    {
        /// <summary>
        /// Azure Mobile Service application URL
        /// </summary>
        public string AmsAppUrl { get; set; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        public string AmsAppKey { get; set; }

        /// <summary>
        /// Assembly hosting model classes (usually Core)
        /// </summary>
        public Assembly ModelAssembly { get; set; }

        /// <summary>
        /// Use single sign on if available (depending of the platform)
        /// </summary>
        public bool UseSingleSignOnIfAvailable { get; set; }
    }
}
