using System.Net.Http;
using Android.App;
using Android.OS;
using Aptk.Plugins.AzureForMobile;
using Aptk.Plugins.AzureForMobile.Identity;
using Aptk.Plugins.AzureForMobile.LocalStore;
using AzureForMobile.Sample.Core;
using AzureForMobile.Sample.Core.Helpers;
using AzureForMobile.Sample.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AzureForMobile.Sample.Droid
{
    [Activity(Label = "AzureForMobile", MainLauncher = true, Icon = "@drawable/logo_rvb-72")]
    public class MainActivity : FormsApplicationActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            InitAzureForMobilePlugin();

            this.LoadApplication(new App());
        }

        private void InitAzureForMobilePlugin()
        {
            var configuration = new AzureForMobilePluginConfiguration(Constants.AmsUrl, Constants.AmsKey, Constants.ModelAssembly);

            // [Optional] Handle expired token to automaticaly ask for login if needed
            var identityHandler = new AzureForMobileIdentityHandler(configuration);
            configuration.Handlers = new HttpMessageHandler[] { identityHandler };

            // [Optional] Handle credentials local caching
            configuration.CredentialsCacheService = new AzureForMobileCredentialCacheService();

            // Init main plugin
            AzureForMobilePluginLoader.Init(configuration, ApplicationContext);

            // [Optional] If AzureForMobileIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AzureForMobileService = AzureForMobilePluginLoader.Instance;
            
            //// Init local store extension
            AzureForMobileLocalStorePluginLoader.Init(new AzureForMobileLocalStorePluginConfiguration(AzureForMobilePluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));
        }
    }
}

