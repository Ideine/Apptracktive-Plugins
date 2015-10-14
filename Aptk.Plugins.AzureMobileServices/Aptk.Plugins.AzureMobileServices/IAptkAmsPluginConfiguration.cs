using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// AptkAms plugin configuration
    /// </summary>
    public interface IAptkAmsPluginConfiguration
    {
        /// <summary>
        /// Azure Mobile Service application URL
        /// </summary>
        string AmsAppUrl { get; set; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        string AmsAppKey { get; set; }

        /// <summary>
        /// Assembly hosting model classes (usually Core)
        /// </summary>
        Assembly ModelAssembly { get; set; }

        /// <summary>
        /// Initialization timeout
        /// </summary>
        /// <value>30sec</value>
        TimeSpan InitTimeout { get; set; }

        /// <summary>
        /// Use single sign on if available (depending of the platform)
        /// </summary>
        bool UseSingleSignOnIfAvailable { get; set; }
    }
}
