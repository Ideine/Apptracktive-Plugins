﻿using System;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Identity;
using Aptk.Plugins.AzureMobileServices.LocalStore;
using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Helpers;
using AptkAms.Sample.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AptkAms.Sample.Droid
{
    [Activity(Label = "AptkAms.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            InitAptkAmsPlugin();

            this.LoadApplication(new App());
        }

        private void InitAptkAmsPlugin()
        {
            var configuration = new AptkAmsPluginConfiguration(Constants.AmsAppUrl, Constants.AmsAppKey, Constants.ModelAssembly);

            // [Optional] Handle expired token to automaticaly ask for login if needed
            var identityHandler = new AptkAmsIdentityHandler(configuration, AptkAmsAuthenticationProvider.Facebook);
            configuration.Handlers = new HttpMessageHandler[] { identityHandler };

            // [Optional] Handle credentials local caching
            configuration.CredentialsCacheService = new AptkAmsCredentialCacheService();

            // Init main plugin
            AptkAmsPluginLoader.Init(configuration, ApplicationContext);

            // [Optional] If AptkAmsIdentityHandler is used, give it an instance of the plugin after Init
            identityHandler.AptkAmsService = AptkAmsPluginLoader.Instance;
            
            //// Init local store extension
            AptkAmsLocalStorePluginLoader.Init(new AptkAmsLocalStorePluginConfiguration(AptkAmsPluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));
        }
    }
}
