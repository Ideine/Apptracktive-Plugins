using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Identity;
using Aptk.Plugins.AzureMobileServices.LocalStore;
using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Helpers;
using AptkAms.Sample.Core.Services;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AptkAms.Sample.Uwp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

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

            AptkAmsPluginLoader.Init(configuration);

            // [Optional] If AptkAmsIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AptkAmsService = AptkAmsPluginLoader.Instance;

            AptkAmsLocalStorePluginLoader.Init(AptkAmsPluginLoader.Instance, new AptkAmsLocalStorePluginConfiguration(Windows.Storage.ApplicationData.Current.LocalFolder.Path));
        }
    }
}
