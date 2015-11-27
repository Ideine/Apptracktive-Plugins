using System.Net.Http;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Identity;
using Aptk.Plugins.AzureMobileServices.LocalStore;
using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Helpers;
using AptkAms.Sample.Core.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace AptkAms.Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            InitAptkAmsPlugin(app);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void InitAptkAmsPlugin(UIApplication app)
        {
            var configuration = new AptkAmsPluginConfiguration(Constants.AmsAppUrl, Constants.AmsAppKey, Constants.ModelAssembly);

            // [Optional] Handle expired token to automaticaly ask for login if needed
            var identityHandler = new AptkAmsIdentityHandler(configuration, AptkAmsAuthenticationProvider.Facebook);
            configuration.Handlers = new HttpMessageHandler[] { identityHandler };

            // [Optional] Handle credentials local caching
            configuration.CredentialsCacheService = new AptkAmsCredentialCacheService();

            AptkAmsPluginLoader.Init(configuration, app);

            // [Optional] If AptkAmsIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AptkAmsService = AptkAmsPluginLoader.Instance;

            AptkAmsLocalStorePluginLoader.Init(AptkAmsPluginLoader.Instance, new AptkAmsLocalStorePluginConfiguration(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));
        }
    }
}