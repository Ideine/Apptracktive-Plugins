using System;
using Aptk.Plugins.AzureForMobile.Api;
using Aptk.Plugins.AzureForMobile.Data;
using Aptk.Plugins.AzureForMobile.Identity;
using Microsoft.WindowsAzure.MobileServices;

#if __IOS__
using UIKit;
#endif

#if __ANDROID__
using Android.App;
using Android.Content;
#endif

namespace Aptk.Plugins.AzureForMobile
{
    public static class AzureForMobilePluginLoader
    {
        private static readonly Lazy<IAzureForMobileService> LazyInstance = new Lazy<IAzureForMobileService>(CreateAzureForMobileService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAzureForMobileApiService> LazyApiInstance = new Lazy<IAzureForMobileApiService>(CreateAzureForMobileApiService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAzureForMobileDataService> LazyDataInstance = new Lazy<IAzureForMobileDataService>(CreateAzureForMobileDataService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAzureForMobileIdentityService> LazyIdentityInstance = new Lazy<IAzureForMobileIdentityService>(CreateAzureForMobileIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAzureForMobilePlatformIdentityService> LazyPlatformIdentityInstance = new Lazy<IAzureForMobilePlatformIdentityService>(CreateAzureForMobilePlatformIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        
        private static IAzureForMobilePluginConfiguration _configuration;
        private static IMobileServiceClient _client;

        #region Init
#if __ANDROID__

        /// <summary>
        /// Initialize Android plugin
        /// </summary>
        public static void Init(IAzureForMobilePluginConfiguration configuration, Context context)
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
        public static void Init(IAzureForMobilePluginConfiguration configuration, UIApplication application)
        {
            _configuration = configuration;
            _client = CreateMobileServiceClient();
            _application = application;
        }

        private static UIApplication _application;

#else

        public static void Init(IAzureForMobilePluginConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateMobileServiceClient();
        }

#endif

        private static IMobileServiceClient CreateMobileServiceClient()
        {
            var client = new MobileServiceClient(_configuration.AmsUrl, _configuration.AmsKey, _configuration.Handlers);

            if (_configuration.SerializerSettings != null)
                client.SerializerSettings = _configuration.SerializerSettings;

            return client;
        }
        #endregion

        #region Instance
        /// <summary>
        /// Current plugin instance
        /// </summary>
        public static IAzureForMobileService Instance
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

        private static IAzureForMobileService CreateAzureForMobileService()
        {
            return new AzureForMobileService(_configuration, _client);
        }
        #endregion

        #region ApiInstance
        /// <summary>
        /// Current Api instance to use
        /// </summary>
        internal static IAzureForMobileApiService ApiInstance
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

        private static IAzureForMobileApiService CreateAzureForMobileApiService()
        {
            return new AzureForMobileApiService(_configuration, _client);
        }
        #endregion

        #region DataInstance
        /// <summary>
        /// Current Data instance to use
        /// </summary>
        internal static IAzureForMobileDataService DataInstance
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

        private static IAzureForMobileDataService CreateAzureForMobileDataService()
        {
            return new AzureForMobileDataService(_configuration, _client);
        }
        #endregion

        #region IdentityInstance
        /// <summary>
        /// Current Identity instance to use
        /// </summary>
        internal static IAzureForMobileIdentityService IdentityInstance
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

        private static IAzureForMobileIdentityService CreateAzureForMobileIdentityService()
        {
            return new AzureForMobileIdentityService(_configuration, _client);
        }
        #endregion

        #region PlatformIdentityInstance
        /// <summary>
        /// Current Platform Identity instance to use
        /// </summary>
        internal static IAzureForMobilePlatformIdentityService PlatformIdentityInstance
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

        private static IAzureForMobilePlatformIdentityService CreateAzureForMobilePlatformIdentityService()
        {
#if PORTABLE
            return null;
#elif __ANDROID__
            return new AzureForMobilePlatformIdentityService(_client, _context);
#elif __IOS__
            return new AzureForMobilePlatformIdentityService(_client, _application);
#else
            return new AzureForMobilePlatformIdentityService(_client);
#endif
        }
        #endregion
    }
}
