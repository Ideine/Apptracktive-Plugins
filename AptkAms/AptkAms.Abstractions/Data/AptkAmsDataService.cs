using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    /// <summary>
    /// Service to manage data
    /// </summary>
    public class AptkAmsDataService : IAptkAmsDataService
    {
        private readonly IAptkAmsPluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;
        private Dictionary<Type, object> _remoteTableServices;
        private bool _isInitilized;

        public AptkAmsDataService(IAptkAmsPluginConfiguration configuration, IMobileServiceClient client)
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
                    Debug.WriteLine("AptkAms: Unable to find any class inheriting ITableData or EntityData into {0}.", _configuration.ModelAssembly.FullName);
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
        public IAptkAmsRemoteTableService<T> RemoteTable<T>() where T : ITableData
        {
            object genericRemoteTable;
            _remoteTableServices.TryGetValue(typeof(T), out genericRemoteTable);
            if (genericRemoteTable == null)
            {
                var remoteTable = new AptkAmsRemoteTableService<T>(_client);
                genericRemoteTable = remoteTable;
                _remoteTableServices.Add(typeof(T), remoteTable);
            }

            return genericRemoteTable as AptkAmsRemoteTableService<T>;
        }
    }
}
