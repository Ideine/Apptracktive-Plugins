using System;
using Aptk.Plugins.AzureMobileServices.Api;
using Aptk.Plugins.AzureMobileServices.Data;
using Aptk.Plugins.AzureMobileServices.Identity;
using Microsoft.WindowsAzure.MobileServices;

#if __IOS__
using UIKit;
#endif

#if __ANDROID__
using Android.App;
using Android.Content;
#endif

namespace Aptk.Plugins.AzureMobileServices
{
    public static class AptkPluginLoader
    {
        private static readonly Lazy<IAptkAmsService> LazyInstance = new Lazy<IAptkAmsService>(CreateAptkAmsService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsApiService> LazyApiInstance = new Lazy<IAptkAmsApiService>(CreateAptkAmsApiService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsDataService> LazyDataInstance = new Lazy<IAptkAmsDataService>(CreateAptkAmsDataService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsIdentityService> LazyIdentityInstance = new Lazy<IAptkAmsIdentityService>(CreateAptkAmsIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsPlatformIdentityService> LazyPlatformIdentityInstance = new Lazy<IAptkAmsPlatformIdentityService>(CreateAptkAmsPlatformIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        
        private static IAptkAmsPluginConfiguration _configuration;
        private static IMobileServiceClient _client;

        #region Init
#if __ANDROID__

        /// <summary>
        /// Initialize Android plugin
        /// </summary>
        public static void Init(IAptkAmsPluginConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _client = CreateMobileServiceClient();
            _context = context;
        }

        private static Context _context;

#elif __IOS__

        /// <summary>
        /// Initialize iOS plugin
        /// </summary>
        public static void Init(IAptkAmsPluginConfiguration configuration, UIApplication application)
        {
            _configuration = configuration;
            _client = CreateMobileServiceClient();
            _application = application;
        }

        private static UIApplication _application;

#else

        public static void Init(IAptkAmsPluginConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateMobileServiceClient();
        }

#endif

        private static IMobileServiceClient CreateMobileServiceClient()
        {
            var client = new MobileServiceClient(_configuration.AmsAppUrl, _configuration.AmsAppKey, _configuration.Handlers);

            if (_configuration.SerializerSettings != null)
                client.SerializerSettings = _configuration.SerializerSettings;

            return client;
        }
        #endregion

        #region Instance
        /// <summary>
        /// Current plugin instance
        /// </summary>
        public static IAptkAmsService Instance
        {
            get
            {
                if (_client == null)
                {
                    throw new ArgumentException("You must call Init method before using it.");
                }
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
            return new AptkAmsService(_configuration, _client);
        }
        #endregion

        #region ApiInstance
        /// <summary>
        /// Current Api instance to use
        /// </summary>
        internal static IAptkAmsApiService ApiInstance
        {
            get
            {
                var apiInstance = LazyApiInstance.Value;
                if (apiInstance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return apiInstance;
            }
        }

        private static IAptkAmsApiService CreateAptkAmsApiService()
        {
            return new AptkAmsApiService(_configuration, _client);
        }
        #endregion

        #region DataInstance
        /// <summary>
        /// Current Data instance to use
        /// </summary>
        internal static IAptkAmsDataService DataInstance
        {
            get
            {
                var dataInstance = LazyDataInstance.Value;
                if (dataInstance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return dataInstance;
            }
        }

        private static IAptkAmsDataService CreateAptkAmsDataService()
        {
            return new AptkAmsDataService(_configuration, _client);
        }
        #endregion

        #region IdentityInstance
        /// <summary>
        /// Current Identity instance to use
        /// </summary>
        internal static IAptkAmsIdentityService IdentityInstance
        {
            get
            {
                var identityInstance = LazyIdentityInstance.Value;
                if (identityInstance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return identityInstance;
            }
        }

        private static IAptkAmsIdentityService CreateAptkAmsIdentityService()
        {
            return new AptkAmsIdentityService(_configuration, _client);
        }
        #endregion

        #region PlatformIdentityInstance
        /// <summary>
        /// Current Platform Identity instance to use
        /// </summary>
        internal static IAptkAmsPlatformIdentityService PlatformIdentityInstance
        {
            get
            {
                var platformIdentityInstance = LazyPlatformIdentityInstance.Value;
                if (platformIdentityInstance == null)
                {
                    throw new ArgumentException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
                }
                return platformIdentityInstance;
            }
        }

        private static IAptkAmsPlatformIdentityService CreateAptkAmsPlatformIdentityService()
        {
#if PORTABLE
            return null;
#elif __ANDROID__
            return new AptkAmsPlatformIdentityService(_client, _context);
#elif __IOS__
            return new AptkAmsPlatformIdentityService(_client, _application);
#else
            return new AptkAmsPlatformIdentityService(_client);
#endif
        }
        #endregion
    }
}
