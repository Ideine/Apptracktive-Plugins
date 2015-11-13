using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Abstractions.Data
{
    public interface IAptkAmsRemoteTableService<T> : IMobileServiceTable<T>, IAptkAmsTableService<T> where T : ITableData
    {
    }
}
