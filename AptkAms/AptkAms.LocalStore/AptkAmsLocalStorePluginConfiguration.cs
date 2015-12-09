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
        /// <param name="aptkAmsServiceInstance"></param>
        /// <param name="databaseFullPath">Database file full device path</param>
        public AptkAmsLocalStorePluginConfiguration(IAptkAmsService aptkAmsServiceInstance, string databaseFullPath)
        {
            AptkAmsServiceInstance = aptkAmsServiceInstance;
            DatabaseFullPath = databaseFullPath;
        }

        /// <summary>
        /// AptkAmsService instance
        /// </summary>
        public IAptkAmsService AptkAmsServiceInstance { get; set; }

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
