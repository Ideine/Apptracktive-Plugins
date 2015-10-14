using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// MvxAms plugin base configuration
    /// </summary>
    public abstract class AptkAmsPluginBaseConfiguration : IAptkAmsPluginConfiguration
    {
        private TimeSpan _initTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Azure Mobile Service application URL
        /// </summary>
        public string AmsAppUrl { get; set; }

        /// <summary>
        /// Azure Mobile Service application Key
        /// </summary>
        public string AmsAppKey { get; set; }

        /// <summary>
        /// Assembly hosting model classes (usually Core)
        /// </summary>
        public Assembly ModelAssembly { get; set; }

        /// <summary>
        /// Initialization timeout
        /// </summary>
        /// <value>30sec</value>
        public TimeSpan InitTimeout
        {
            get { return _initTimeout; }
            set { _initTimeout = value; }
        }

        /// <summary>
        /// Use single sign on if available (depending of the platform)
        /// </summary>
        public bool UseSingleSignOnIfAvailable { get; set; }
    }
}
