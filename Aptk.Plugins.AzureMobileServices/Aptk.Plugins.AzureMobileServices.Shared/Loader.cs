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
    public static class Loader
    {
        private static readonly Lazy<IAptkAmsService> LazyInstance = new Lazy<IAptkAmsService>(CreateAptkAmsService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsApiService> LazyApiInstance = new Lazy<IAptkAmsApiService>(CreateAptkAmsApiService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsDataService> LazyDataInstance = new Lazy<IAptkAmsDataService>(CreateAptkAmsDataService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsIdentityService> LazyIdentityInstance = new Lazy<IAptkAmsIdentityService>(CreateAptkAmsIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IAptkAmsPlatformIdentityService> LazyPlatformIdentityInstance = new Lazy<IAptkAmsPlatformIdentityService>(CreateAptkAmsPlatformIdentityService, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static readonly Lazy<IMobileServiceClient> LazyClientInstance = new Lazy<IMobileServiceClient>(CreateMobileServiceClient, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        #region Init
#if __ANDROID__

        /// <summary>
        /// Initialize Android plugin
        /// </summary>
        public static void Init(IAptkAmsPluginConfiguration configuration, Context context)
        {
            Configuration = configuration;
            _clientInstance = CreateMobileServiceClient();
            Context = context;
        }

        internal static Context Context { get; set; }

#elif __IOS__

        /// <summary>
        /// Initialize iOS plugin
        /// </summary>
        public static void Init(IAptkAmsPluginConfiguration configuration, UIViewController rootViewController)
        {
            Configuration = configuration;
            _clientInstance = CreateMobileServiceClient();
            RootViewController = rootViewController;
        }

        internal static UIViewController RootViewController { get; set; }

#else

        public static void Init(IAptkAmsPluginConfiguration configuration)
        {
            Configuration = configuration;
            _clientInstance = CreateMobileServiceClient();
        }

#endif

        internal static IAptkAmsPluginConfiguration Configuration { get; private set; } 
        #endregion

        #region Instance
        /// <summary>
        /// Current plugin instance
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
            return new AptkAmsService();
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
            return new AptkAmsApiService();
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
            return new AptkAmsDataService();
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
            return new AptkAmsIdentityService();
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
            return new AptkAmsPlatformIdentityService(ClientInstance, Context);
#elif __IOS__
            return new AptkAmsPlatformIdentityService(ClientInstance, RootViewController);
#elif WINDOWS_PHONE
            return new AptkAmsPlatformIdentityService(ClientInstance);
#else
            return new AptkAmsPlatformIdentityService(ClientInstance, Configuration.UseSingleSignOnIfAvailable);
#endif
        }
        #endregion

        #region ClientInstance

        /// <summary>
        /// Current Client instance to use
        /// </summary>
        private static IMobileServiceClient _clientInstance;
        public static IMobileServiceClient ClientInstance
        {
            get { return _clientInstance ?? LazyClientInstance.Value; }
            set { _clientInstance = value; }
        }

        private static IMobileServiceClient CreateMobileServiceClient()
        {
            if (Configuration == null)
            {
                throw new ArgumentException("Your must specify an IAptkAmsPluginConfiguration implementation to make this plugin work");
            }
            return new MobileServiceClient(Configuration.AmsAppUrl, Configuration.AmsAppKey);
        }

        #endregion
    }
}
