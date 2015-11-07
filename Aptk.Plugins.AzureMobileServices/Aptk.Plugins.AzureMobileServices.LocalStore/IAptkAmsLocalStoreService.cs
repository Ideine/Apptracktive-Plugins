using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices.Abstractions.Data;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    public interface IAptkAmsLocalStoreService
    {
        Task<bool> InitializationTask { get; }

        IAptkAmsLocalTableService<T> GetLocalTable<T>() where T : ITableData;
        
        Task PushAsync();

        long PendingOperations { get; }
    }
}
