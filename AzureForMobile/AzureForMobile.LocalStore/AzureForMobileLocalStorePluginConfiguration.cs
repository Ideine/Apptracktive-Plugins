using System;

namespace Aptk.Plugins.AzureForMobile.LocalStore
{
    /// <summary>
    /// Azure For Mobile Plugin's Local Store Extension configuration
    /// </summary>
    public class AzureForMobileLocalStorePluginConfiguration : IAzureForMobileLocalStorePluginConfiguration
    {
        /// <summary>
        /// Azure For Mobile Plugin's Local Store Extension constructor
        /// </summary>
        /// <param name="azureForMobilePluginInstance">Instance of the main Azure For Mobile Plugin</param>
        /// <param name="databaseFullPath">Database file full device path</param>
        public AzureForMobileLocalStorePluginConfiguration(IAzureForMobileService azureForMobilePluginInstance, string databaseFullPath)
        {
            AzureForMobilePluginInstance = azureForMobilePluginInstance;
            DatabaseFullPath = databaseFullPath;
        }

        /// <summary>
        /// Azure For Mobile Plugin instance
        /// </summary>
        public IAzureForMobileService AzureForMobilePluginInstance { get; set; }

        /// <summary>
        /// Database file full device path
        /// </summary>
        public string DatabaseFullPath { get; set; }

        /// <summary>
        /// Database file name with db extension
        /// </summary>
        /// <value>amslocalstore.db</value>
        public string DatabaseFileName { get; set; } = "amslocalstore.db";

        /// <summary>
        /// Initialization timeout
        /// </summary>
        /// <value>30sec</value>
        public TimeSpan InitTimeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
