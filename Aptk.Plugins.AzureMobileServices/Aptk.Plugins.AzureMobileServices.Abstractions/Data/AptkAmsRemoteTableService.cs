using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Abstractions.Data
{
    public class AptkAmsRemoteTableService<T> : IAptkAmsRemoteTableService<T>
    {
        private readonly IMobileServiceClient _client;
        private IMobileServiceTable<T> _remoteTable;
        private bool _isInitialized;

        public AptkAmsRemoteTableService(IMobileServiceClient client)
        {
            _client = client;
        }

        private bool Initialize()
        {
            if (!_isInitialized)
            {
                try
                {
                    _remoteTable = _client.GetTable<T>();
                    _isInitialized = true;
                }
                catch (Exception)
                {
                }
            }

            return _isInitialized;
        }

        public async Task<MobileServiceCollection<T, T>> ToCollectionAsync(Func<IMobileServiceTableQuery<T>, IMobileServiceTableQuery<T>> query = null)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to retrieve your data. Initialization failed.", null, null);

            return query == null ? await _remoteTable.CreateQuery().ToCollectionAsync() : await query(_remoteTable.CreateQuery()).ToCollectionAsync();
        }

        public async Task<IList<T>> ToListAsync(Func<IMobileServiceTableQuery<T>, IMobileServiceTableQuery<T>> query)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to retrieve your data. Initialization failed.", null, null);

            return query == null ? await _remoteTable.CreateQuery().ToListAsync() : await query(_remoteTable.CreateQuery()).ToListAsync();
        }

        public async Task<IEnumerable<T>> ToEnumerableAsync(Func<IMobileServiceTableQuery<T>, IMobileServiceTableQuery<T>> query)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to retrieve your data. Initialization failed.", null, null);

            return query == null ? await _remoteTable.CreateQuery().ToEnumerableAsync() : await query(_remoteTable.CreateQuery()).ToEnumerableAsync();
        }

        public async Task<T> LookupAsync(string id)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to retrieve your data. Initialization failed.", null, null);

            return await _remoteTable.LookupAsync(id);
        }

        public async Task RefreshAsync(T instance)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to refresh your data. Initialization failed.", null, null);

            await _remoteTable.RefreshAsync(instance);
        }

        public async Task InsertAsync(T instance)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to insert your data. Initialization failed.", null, null);

            await _remoteTable.InsertAsync(instance);
        }

        public async Task UpdateAsync(T instance)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to update your data. Initialization failed.", null, null);

            await _remoteTable.UpdateAsync(instance);
        }

        public async Task DeleteAsync(T instance)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to delete your data. Initialization failed.", null, null);

            await _remoteTable.DeleteAsync(instance);
        }

        public async Task UndeleteAsync(T instance)
        {
            if (!Initialize())
                throw new MobileServiceInvalidOperationException("Unable to undelete your data. Initialization failed.", null, null);

            await _remoteTable.UndeleteAsync(instance);
        }
    }
}
