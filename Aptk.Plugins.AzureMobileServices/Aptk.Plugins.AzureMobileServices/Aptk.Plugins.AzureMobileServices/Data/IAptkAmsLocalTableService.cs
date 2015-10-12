using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    /// <summary>
    /// Local specific data request service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAptkAmsLocalTableService<T> : IAptkAmsTableService<T>
    {
        /// <summary>
        /// Pulls all items from remote Azure table matching the optional query
        /// </summary>
        /// <param name="query">Optional query to filter items to pull</param>
        /// <returns></returns>
        Task Pull(Func<IMobileServiceTableQuery<T>, IMobileServiceTableQuery<T>> query = null);

        /// <summary>
        /// Deletes all items from local SQLite table
        /// </summary>
        /// <param name="force">Force deletion</param>
        /// <returns></returns>
        Task Purge(bool force = false);
    }
}
