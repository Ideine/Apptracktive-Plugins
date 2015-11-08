namespace Aptk.Plugins.AzureMobileServices.Abstractions.Data
{
    /// <summary>
    /// Service to manage data
    /// </summary>
    public interface IAptkAmsDataService
    {
        /// <summary>
        /// Service to manage remote Azure data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        IAptkAmsRemoteTableService<T> RemoteTable<T>() where T : ITableData;
    }
}
