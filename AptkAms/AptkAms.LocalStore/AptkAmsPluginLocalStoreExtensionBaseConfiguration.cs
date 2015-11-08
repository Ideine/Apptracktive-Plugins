using System;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    /// <summary>
    /// AptkAms Local Store plugin base configuration
    /// </summary>
    public abstract class AptkAmsPluginLocalStoreExtensionBaseConfiguration : IAptkAmsPluginLocalStoreExtensionConfiguration
    {
        protected AptkAmsPluginLocalStoreExtensionBaseConfiguration(string databaseFullPath)
        {
            DatabaseFullPath = databaseFullPath;
        }

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
