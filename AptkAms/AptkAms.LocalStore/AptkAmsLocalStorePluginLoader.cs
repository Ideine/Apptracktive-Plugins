using System;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Data;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    public static class AptkAmsLocalStorePluginLoader
    {
        private static readonly Lazy<IAptkAmsLocalStoreService> LazyInstance = new Lazy<IAptkAmsLocalStoreService>(CreateAptkAmsLocalStoreService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        
        private static IAptkAmsLocalStorePluginConfiguration _localStoreConfiguration;

        public static void Init(IAptkAmsLocalStorePluginConfiguration localStoreConfiguration)
        {
            _localStoreConfiguration = localStoreConfiguration;
        }

        private static IAptkAmsLocalStoreService CreateAptkAmsLocalStoreService()
        {
            return new AptkAmsLocalStoreService(_localStoreConfiguration);
        }

        /// <summary>
        /// Current plugin instance
        /// </summary>
        private static IAptkAmsLocalStoreService Instance
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
        public static IAptkAmsLocalTableService<T> LocalTable<T>(this IAptkAmsDataService dataService) where T : ITableData
        {
            return Instance.GetLocalTable<T>();
        }
        
        /// <summary>
        /// Push local pending changes to remote Azure tables
        /// </summary>
        /// <returns></returns>
        public static async Task PushAsync(this IAptkAmsDataService dataService)
        {
            await Instance.PushAsync();
        }

        /// <summary>
        /// Local pending changes waiting for push to remote Azure tables
        /// </summary>
        public static long PendingChanges(this IAptkAmsDataService dataService) => Instance.PendingOperations;

    }
}
