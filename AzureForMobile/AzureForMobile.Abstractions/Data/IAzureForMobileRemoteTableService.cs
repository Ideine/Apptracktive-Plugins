using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Data
{
    public interface IAzureForMobileRemoteTableService<T> : IMobileServiceTable<T>, IAzureForMobileTableService<T> where T : ITableData
    {
    }
}
