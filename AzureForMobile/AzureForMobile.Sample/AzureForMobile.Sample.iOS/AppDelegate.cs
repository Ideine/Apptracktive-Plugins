using System.Net.Http;
using Aptk.Plugins.AzureForMobile;
using Aptk.Plugins.AzureForMobile.Identity;
using Aptk.Plugins.AzureForMobile.LocalStore;
using AzureForMobile.Sample.Core;
using AzureForMobile.Sample.Core.Helpers;
using AzureForMobile.Sample.Core.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace AzureForMobile.Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            InitAzureForMobilePlugin(app);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void InitAzureForMobilePlugin(UIApplication app)
        {
            var configuration = new AzureForMobilePluginConfiguration(Constants.AmsUrl, Constants.AmsKey, Constants.ModelAssembly);

            // [Optional] Handle expired token to automaticaly ask for login if needed
            var identityHandler = new AzureForMobileIdentityHandler(configuration, AzureForMobileAuthenticationProvider.Facebook);
            configuration.Handlers = new HttpMessageHandler[] { identityHandler };

            // [Optional] Handle credentials local caching
            configuration.CredentialsCacheService = new AzureForMobileCredentialCacheService();

            // Init main plugin
            AzureForMobilePluginLoader.Init(configuration, app);

            // [Optional] If AzureForMobileIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AzureForMobileService = AzureForMobilePluginLoader.Instance;

            //// Init SQLite
            SQLitePCL.CurrentPlatform.Init();

            //// Init local store extension
            AzureForMobileLocalStorePluginLoader.Init(new AzureForMobileLocalStorePluginConfiguration(AzureForMobilePluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));
        }
    }
}


