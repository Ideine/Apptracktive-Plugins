using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Data
{
    /// <summary>
    /// Service to manage data
    /// </summary>
    public class AzureForMobileDataService : IAzureForMobileDataService
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;
        private Dictionary<Type, object> _remoteTableServices;
        private bool _isInitilized;

        public AzureForMobileDataService(IAzureForMobilePluginConfiguration configuration, IMobileServiceClient client)
        {
            _configuration = configuration;
            _client = client;

            // Init tables
            Initialize();
        }

        private bool Initialize()
        {
            if (!_isInitilized)
            {
                _remoteTableServices = new Dictionary<Type, object>();

                // Get the list of tables
                List<Type> tableTypes;
                try
                {
                    tableTypes = _configuration.ModelAssembly.DefinedTypes.Where(type => typeof(ITableData).GetTypeInfo().IsAssignableFrom(type)).Select(t => t.AsType()).ToList();
                }
                catch (Exception)
                {
                    Debug.WriteLine("AzureForMobile: Unable to find any class inheriting ITableData or EntityData into {0}.", _configuration.ModelAssembly.FullName);
                    return false;
                }

                // Get tables
                foreach (var tableType in tableTypes)
                {
                    // Get remote table
                    GetType().GetTypeInfo().GetDeclaredMethod("RemoteTable").MakeGenericMethod(tableType).Invoke(this, null);
                }

                _isInitilized = true;
            }
            return _isInitilized;
        }

        /// <summary>
        /// Service to manage remote Azure data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        public IAzureForMobileRemoteTableService<T> RemoteTable<T>() where T : ITableData
        {
            object genericRemoteTable;
            _remoteTableServices.TryGetValue(typeof(T), out genericRemoteTable);
            if (genericRemoteTable == null)
            {
                var remoteTable = new AzureForMobileRemoteTableService<T>(_client);
                genericRemoteTable = remoteTable;
                _remoteTableServices.Add(typeof(T), remoteTable);
            }

            return genericRemoteTable as AzureForMobileRemoteTableService<T>;
        }
    }
}
