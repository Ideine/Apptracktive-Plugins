using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptk.Plugins.AzureMobileServices.Data
{
    public interface IAptkAmsLocalStoreService
    {
        Task<bool> InitializeAsync(List<Type> tableTypes = null);

        IAptkAmsLocalTableService<T> GetLocalTable<T>();
    }
}
