using System.Threading;
using System.Threading.Tasks;
using Aptk.Plugins.AzureForMobile.Data;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Aptk.Plugins.AzureForMobile.LocalStore
{
    /// <summary>
    /// Local specific data request service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAzureForMobileLocalTableService<T> : IMobileServiceSyncTable<T>, IAzureForMobileTableService<T> where T : ITableData
    {
        Task PurgeAsync<U>(IMobileServiceTableQuery<U> query, CancellationToken cancellationToken);

        Task PurgeAsync(string queryId, CancellationToken cancellationToken);

        Task PurgeAsync(CancellationToken cancellationToken);

        Task PullAsync(string queryId, bool pushOtherTables, CancellationToken cancellationToken);

        Task PullAsync<U>(string queryId, IMobileServiceTableQuery<U> query, CancellationToken cancellationToken);

        Task PullAsync<U>(IMobileServiceTableQuery<U> query, bool pushOtherTables, CancellationToken cancellationToken);

        Task PullAsync(string queryId, CancellationToken cancellationToken);

        Task PullAsync<U>(IMobileServiceTableQuery<U> query, CancellationToken cancellationToken);

        Task PullAsync(bool pushOtherTables, CancellationToken cancellationToken);

        Task PullAsync(CancellationToken cancellationToken);
    }
}
