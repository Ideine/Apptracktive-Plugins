﻿using Android.App;
using Android.OS;
using AptkAms.Sample.Core;
using AptkAms.Sample.Core.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AptkAms.Sample.Droid
{
    [Activity(Label = "AptkAms.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            Aptk.Plugins.AzureMobileServices.Loader.Init(new AptkAmsPluginConfiguration(), ApplicationContext);
            this.LoadApplication(new App());
        }
    }
}

