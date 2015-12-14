using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Aptk.Plugins.AzureMobileServices;
using AptkAms.Sample.Core;
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

            var configuration = new AptkAmsPluginConfiguration(Constants.AmsAppUrl, Constants.AmsAppKey, Constants.ModelAssembly);
            AptkAmsPluginLoader.Init(configuration, ApplicationContext);

            this.LoadApplication(new App());
        }
    }
}

