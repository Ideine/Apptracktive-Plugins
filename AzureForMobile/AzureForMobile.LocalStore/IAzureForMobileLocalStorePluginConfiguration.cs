using System;

namespace Aptk.Plugins.AzureForMobile.LocalStore
{
    /// <summary>
    /// Azure For Mobile Plugin's Local Store Extension configuration
    /// </summary>
    public interface IAzureForMobileLocalStorePluginConfiguration
    {
        /// <summary>
        /// Azure For Mobile Plugin instance
        /// </summary>
        IAzureForMobileService AzureForMobilePluginInstance { get; set; }

        /// <summary>
        /// Database file full device path
        /// </summary>
        string DatabaseFullPath { get; set; }

        /// <summary>
        /// Database file name with db extension
        /// </summary>
        /// <value>amslocalstore.db</value>
        string DatabaseFileName { get; set; }

        /// <summary>
        /// Initialization timeout
        /// </summary>
        /// <value>30sec</value>
        TimeSpan InitTimeout { get; set; }
    }
}
