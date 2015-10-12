using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    public interface IAptkAmsRemoteTableService<T> : IAptkAmsTableService<T>
    {
        /// <summary>
        /// Undelete an instance from the remote Azure table if soft delete is enabled
        /// </summary>
        /// <param name="instance">Instance to undelete from table</param>
        /// <returns></returns>
        Task UndeleteAsync(T instance);
    }
}
