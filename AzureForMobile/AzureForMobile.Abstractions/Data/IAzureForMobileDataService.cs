namespace Aptk.Plugins.AzureForMobile.Data
{
    /// <summary>
    /// Service to manage data
    /// </summary>
    public interface IAzureForMobileDataService
    {
        /// <summary>
        /// Service to manage remote Azure data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        IAzureForMobileRemoteTableService<T> RemoteTable<T>() where T : ITableData;
    }
}
