using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    public class AptkAmsDataService : IAptkAmsDataService
    {
        private readonly IAptkAmsPluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;
        private List<IAptkAmsRemoteTableService<ITableData>> _remoteTableServices;
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
                _remoteTableServices = new List<IAptkAmsRemoteTableService<ITableData>>();

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
            var remoteTable = _remoteTableServices.FirstOrDefault(t => t is IAptkAmsRemoteTableService<T>);
            if (remoteTable == null)
            {
                remoteTable = (IAptkAmsRemoteTableService<ITableData>) new AptkAmsRemoteTableService<T>(_client);
                _remoteTableServices.Add(remoteTable);
            }
            return remoteTable as AptkAmsRemoteTableService<T>;
        }
    }
}
