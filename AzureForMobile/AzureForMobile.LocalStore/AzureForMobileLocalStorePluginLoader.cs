using System;
using System.Threading.Tasks;
using Aptk.Plugins.AzureForMobile.Data;

namespace Aptk.Plugins.AzureForMobile.LocalStore
{
    public static class AzureForMobileLocalStorePluginLoader
    {
        private static readonly Lazy<IAzureForMobileLocalStoreService> LazyInstance = new Lazy<IAzureForMobileLocalStoreService>(CreateAzureForMobileLocalStoreService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        
        private static IAzureForMobileLocalStorePluginConfiguration _localStoreConfiguration;

        public static void Init(IAzureForMobileLocalStorePluginConfiguration localStoreConfiguration)
        {
            _localStoreConfiguration = localStoreConfiguration;
        }

        private static IAzureForMobileLocalStoreService CreateAzureForMobileLocalStoreService()
        {
            return new AzureForMobileLocalStoreService(_localStoreConfiguration);
        }

        /// <summary>
        /// Current plugin instance
        /// </summary>
        private static IAzureForMobileLocalStoreService Instance
        {
            get
            {
                var instance = LazyInstance.Value;
                if (instance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return instance;
            }
        }


        /// <summary>
        /// Service to manage local SQLite data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        public static IAzureForMobileLocalTableService<T> LocalTable<T>(this IAzureForMobileDataService dataService) where T : ITableData
        {
            return Instance.GetLocalTable<T>();
        }
        
        /// <summary>
        /// Push local pending changes to remote Azure tables
        /// </summary>
        /// <returns></returns>
        public static async Task PushAsync(this IAzureForMobileDataService dataService)
        {
            await Instance.PushAsync();
        }

        /// <summary>
        /// Local pending changes waiting for push to remote Azure tables
        /// </summary>
        public static long PendingChanges(this IAzureForMobileDataService dataService) => Instance.PendingOperations;

    }
}
