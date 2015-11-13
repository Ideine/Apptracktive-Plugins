using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Abstractions.Data
{
    public class AptkAmsDataService : IAptkAmsDataService
    {
        private readonly IAptkAmsPluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;
        private List<object> _remoteTableServices;
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
                _remoteTableServices = new List<object>();

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

        public IAptkAmsRemoteTableService<T> RemoteTable<T>() where T : ITableData
        {
            var genericRemoteTable = _remoteTableServices.FirstOrDefault(t => t is AptkAmsRemoteTableService<T>);
            if (genericRemoteTable == null)
            {
                var remoteTable = new AptkAmsRemoteTableService<T>(_client);
                genericRemoteTable = remoteTable;
                _remoteTableServices.Add(remoteTable);
            }
            return genericRemoteTable as AptkAmsRemoteTableService<T>;
        }
    }
}
