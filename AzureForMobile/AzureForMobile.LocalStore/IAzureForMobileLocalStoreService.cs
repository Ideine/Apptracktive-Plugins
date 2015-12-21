using System.Threading.Tasks;
using Aptk.Plugins.AzureForMobile.Data;

namespace Aptk.Plugins.AzureForMobile.LocalStore
{
    public interface IAzureForMobileLocalStoreService
    {
        Task<bool> InitializationTask { get; }

        IAzureForMobileLocalTableService<T> GetLocalTable<T>() where T : ITableData;
        
        Task PushAsync();

        long PendingOperations { get; }
    }
}
