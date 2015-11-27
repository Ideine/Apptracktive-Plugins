using System.Net.Http;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Identity;
using Aptk.Plugins.AzureMobileServices.LocalStore;
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

            this.LoadApplication(new Core.App());
        }
    }
}