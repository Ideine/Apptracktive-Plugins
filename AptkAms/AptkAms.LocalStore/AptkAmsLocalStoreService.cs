using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Data;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    public class AptkAmsLocalStoreService : IAptkAmsLocalStoreService
    {
        private readonly IAptkAmsPluginConfiguration _configuration;
        private readonly IAptkAmsLocalStorePluginConfiguration _localStoreConfiguration;
        private MobileServiceSQLiteStore _localStore;
        private readonly IMobileServiceClient _client;
        private List<object> _localTableServices;

        public AptkAmsLocalStoreService(IAptkAmsLocalStorePluginConfiguration localStoreConfiguration)
        {
            _localStoreConfiguration = localStoreConfiguration;
            _configuration = localStoreConfiguration.AptkAmsServiceInstance.Configuration;
            _client = localStoreConfiguration.AptkAmsServiceInstance.Client;

            InitializationTask = InitializeAsync();
            Task.Run(async () => await InitializationTask);
        }

        public Task<bool> InitializationTask { get; }

        private async Task<bool> InitializeAsync()
        {
            if (!_client.SyncContext.IsInitialized)
            {
                _localTableServices = new List<object>();

                // Init local store
                var fullPath = Path.Combine(_localStoreConfiguration.DatabaseFullPath, _localStoreConfiguration.DatabaseFileName);
                try
                {
                    _localStore = new MobileServiceSQLiteStore(fullPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("AptkAms: Unable to create database file {0}. Local store initialization terminated with error: {1}", fullPath, ex.Message);
                    return false;
                }
                
                // Get the list of tables
                List<Type> tableTypes;
                try
                {
                    tableTypes = _configuration.ModelAssembly.DefinedTypes.Where(typeInfo => typeof(ITableData).GetTypeInfo().IsAssignableFrom(typeInfo)).Select(typeInfo => typeInfo.AsType()).ToList();
                }
                catch (Exception)
                {
                    Debug.WriteLine("AptkAms: Unable to find any class inheriting ITableData or EntityData into {0}.", _configuration.ModelAssembly.FullName);
                    return false;
                }

                // Define local tables
                foreach (var tableType in tableTypes)
                {
                    GetType().GetTypeInfo().GetDeclaredMethod("DefineTable").MakeGenericMethod(tableType).Invoke(this, null);
                }

                var syncHandlerTypeInfo = _configuration.ModelAssembly.DefinedTypes.FirstOrDefault(typeInfo => typeof(IMobileServiceSyncHandler).GetTypeInfo().IsAssignableFrom(typeInfo));
                var syncHandler = syncHandlerTypeInfo != null ? (IMobileServiceSyncHandler)Activator.CreateInstance(syncHandlerTypeInfo.AsType()) : new MobileServiceSyncHandler();

                // Init local store
                try
                {
                    await _client.SyncContext.InitializeAsync(_localStore, syncHandler);
                }
                catch (Exception)
                {
                    Debug.WriteLine("AptkAms: Unable to initialize local store. Please refer to online documentation.");
                }
            }
            return _client.SyncContext.IsInitialized;
        }

        private void DefineTable<T>()
        {
            _localStore.DefineTable<T>();
        }

        public IAptkAmsLocalTableService<T> GetLocalTable<T>() where T : ITableData
        {
            var genericLocalTable = _localTableServices.FirstOrDefault(t => t is AptkAmsLocalTableService<T>);
            if (genericLocalTable == null)
            {
                var localTable = new AptkAmsLocalTableService<T>(_localStoreConfiguration, _client, this);
                genericLocalTable = localTable;
                _localTableServices.Add(localTable);
            }

            return genericLocalTable as AptkAmsLocalTableService<T>;
        }

        public async Task PushAsync()
        {
            var cts = new CancellationTokenSource();
            await Task.WhenAny(InitializationTask, Task.Delay(_localStoreConfiguration.InitTimeout, cts.Token));

            if (InitializationTask.IsCompleted && _client.SyncContext.IsInitialized)
            {
                await _client.SyncContext.PushAsync(cts.Token);
            }
        }


        public long PendingOperations => _client.SyncContext.PendingOperations;
    }
}
