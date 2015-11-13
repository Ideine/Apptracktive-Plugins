using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Configuration;
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
            Aptk.Plugins.AzureMobileServices.Loader.Init(new AptkAmsPluginConfiguration(), app);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}