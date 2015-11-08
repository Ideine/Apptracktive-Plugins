using Aptk.Plugins.AzureMobileServices.Abstractions.Data;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Aptk.Plugins.AzureMobileServices.LocalStore
{
    /// <summary>
    /// Local specific data request service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAptkAmsLocalTableService<T> : IMobileServiceSyncTable<T>, IAptkAmsTableService<T>
    {
    }
}
