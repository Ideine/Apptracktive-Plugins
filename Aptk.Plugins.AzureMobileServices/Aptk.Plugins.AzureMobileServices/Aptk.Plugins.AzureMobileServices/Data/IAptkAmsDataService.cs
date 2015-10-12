using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    /// <summary>
    /// Service to manage data
    /// </summary>
    public interface IAptkAmsDataService
    {
        /// <summary>
        /// Service to manage local SQLite data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        IAptkAmsLocalTableService<T> LocalTable<T>();

        /// <summary>
        /// Service to manage remote Azure data
        /// </summary>
        /// <typeparam name="T">Data table to manage (model class)</typeparam>
        /// <returns></returns>
        IAptkAmsRemoteTableService<T> RemoteTable<T>();

        /// <summary>
        /// Push local pending changes to remote Azure tables
        /// </summary>
        /// <returns></returns>
        Task PushAsync();

        /// <summary>
        /// Local pending changes waiting for push to remote Azure tables
        /// </summary>
        long PendingOperations { get; }
    }
}
