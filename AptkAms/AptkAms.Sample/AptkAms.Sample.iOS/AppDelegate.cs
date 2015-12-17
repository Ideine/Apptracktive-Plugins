using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.LocalStore;
using AptkAms.Sample.Core;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace AptkAms.Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            var configuration = new AptkAmsPluginConfiguration(Constants.AmsAppUrl, Constants.AmsAppKey, Constants.ModelAssembly);
            AptkAmsPluginLoader.Init(configuration, app);

            SQLitePCL.CurrentPlatform.Init();
            AptkAmsLocalStorePluginLoader.Init(new AptkAmsLocalStorePluginConfiguration(AptkAmsPluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}


