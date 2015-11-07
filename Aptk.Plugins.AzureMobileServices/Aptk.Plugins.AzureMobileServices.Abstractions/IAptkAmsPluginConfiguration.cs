﻿using System.Reflection;

namespace Aptk.Plugins.AzureMobileServices.Abstractions
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
    }
}
