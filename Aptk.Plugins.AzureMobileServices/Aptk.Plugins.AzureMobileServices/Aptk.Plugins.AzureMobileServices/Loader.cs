using System;

namespace Aptk.Plugins.AzureMobileServices
{
    public static class Loader
    {
        private static readonly Lazy<IAptkAmsService> LazyInstance = new Lazy<IAptkAmsService>(CreateAptkAmsService, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IAptkAmsService Instance
        {
            get
            {
                var instance = LazyInstance.Value;
                if (instance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return instance;
            }
        }

        private static IAptkAmsService CreateAptkAmsService()
        {
        #if PORTABLE
            return null;
        #else
            return new AptkAmsService();
        #endif
        }
    }
}
