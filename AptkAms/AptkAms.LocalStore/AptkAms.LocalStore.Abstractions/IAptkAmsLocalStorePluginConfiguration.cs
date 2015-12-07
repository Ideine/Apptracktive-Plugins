using System;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    /// <summary>
    /// AptkAms Local Store plugin configuration
    /// </summary>
    public interface IAptkAmsLocalStorePluginConfiguration
    {
        /// <summary>
        /// Instance of AptkAmsService
        /// </summary>
        IAptkAmsService AptkAmsPluginInstance { get; }

        /// <summary>
        /// Database file short device path
        /// </summary>
        string DatabaseShortPath { get; set; }

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
