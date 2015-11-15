using System.Net.Http;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Identity;
using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Helpers;
using AptkAms.Sample.Core.Services;
using Xamarin.Forms;

namespace AptkAms.Sample.WinPhone8
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            InitAptkAmsPlugin();

            this.LoadApplication(new Core.App());
        }

        private void InitAptkAmsPlugin()
        {
            var configuration = new AptkAmsPluginConfiguration(Constants.AmsAppUrl, Constants.AmsAppKey, Constants.ModelAssembly);

            // [Optional] Handle expired token to automaticaly ask for login if needed
            var identityHandler = new AptkAmsIdentityHandler(configuration, AptkAmsAuthenticationProvider.Facebook);
            configuration.Handlers = new HttpMessageHandler[] { identityHandler };

            // [Optional] Handle credentials local caching
            configuration.CredentialsCacheService = new AptkAmsCredentialCacheService();

            AptkPluginLoader.Init(configuration);

            // [Optional] If AptkAmsIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AptkAmsService = AptkPluginLoader.Instance;
        }
    }
}