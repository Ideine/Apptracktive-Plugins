using System;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    /// <summary>
    /// AptkAms Local Store plugin configuration
    /// </summary>
    public class AptkAmsLocalStorePluginConfiguration : IAptkAmsLocalStorePluginConfiguration
    {
        /// <summary>
        /// AptkAms Local Store plugin constructor
        /// </summary>
        /// <param name="aptkAmsPluginInstance">Instance of AptkAmsService</param>
        public AptkAmsLocalStorePluginConfiguration(IAptkAmsService aptkAmsPluginInstance)
        {
            AptkAmsPluginInstance = aptkAmsPluginInstance;
        }

        /// <summary>
        /// Instance of AptkAmsService
        /// </summary>
        public IAptkAmsService AptkAmsPluginInstance { get; }

        /// <summary>
        /// Database file full device path
        /// </summary>
        public string DatabaseShortPath { get; set; }

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
