using System.Reflection;
using Aptk.Plugins.AzureMobileServices.Abstractions;

namespace AptkAms.Sample.Core.Configuration
{
    public class AptkAmsPluginConfiguration : AptkAmsPluginBaseConfiguration
    {
        public AptkAmsPluginConfiguration()
        {
            // [Mandatory] Set the Azure Mobile Services application Url
            AmsAppUrl = "YOUR URL HERE";

            // [Mandatory] Set the Azure Mobile Services application Key
            AmsAppKey = "YOUR KEY HERE";

            // [Mandatory] Tell the plugin which Assembly host your Model classes
            ModelAssembly = GetType().GetTypeInfo().Assembly;

            // [Optional] Credential cache service to save, load and clear credentials from device
            //CredentialsCacheService = new AptkAmsCredentialCacheService();

            // [Optional] Add a custom handler if you need to work on http massages (Facebook authentication handling in this example)
            //Handlers = new HttpMessageHandler[] { new AptkAmsIdentityHandler(AptkAmsAuthenticationProvider.Facebook) };

            // [Optional] Specify your own json serializer settings
            //SerializerSettings = new MobileServiceJsonSerializerSettings
            //{
            //    CamelCasePropertyNames = true
            //};

            // [Optional] Set your own plugin initialization (default: 30 sec)
            //InitTimeout = TimeSpan.FromSeconds(60);
        }

        ///// <summary>
        ///// This IAptkAmsCredentialsCacheService implementation is a working example 
        ///// requiring the installation of Xamarin Settings plugin.
        ///// Uncomment if you want to use it
        ///// </summary>
        //class AptkAmsCredentialCacheService : IAptkAmsCredentialsCacheService
        //{
        //    public bool TryLoadCredentials(out IAptkAmsCredentials credentials)
        //    {
        //        credentials = !string.IsNullOrEmpty(Settings.AptkAmsIdentityUserId)
        //                      && !string.IsNullOrEmpty(Settings.AptkAmsIdentityAuthToken)
        //                      && Settings.AptkAmsIdentityProvider != AptkAmsAuthenticationProvider.None
        //            ? new AptkAmsCredentials
        //            {
        //                Provider = Settings.AptkAmsIdentityProvider,
        //                User = new MobileServiceUser(Settings.AptkAmsIdentityUserId)
        //                {
        //                    MobileServiceAuthenticationToken = Settings.AptkAmsIdentityAuthToken
        //                }
        //            }
        //            : null;

        //        return credentials != null;
        //    }

        //    public void SaveCredentials(IAptkAmsCredentials credentials)
        //    {
        //        if (credentials == null)
        //            return;

        //        Settings.AptkAmsIdentityProvider = credentials.Provider;
        //        Settings.AptkAmsIdentityUserId = credentials.User.UserId;
        //        Settings.AptkAmsIdentityAuthToken = credentials.User.MobileServiceAuthenticationToken;
        //    }

        //    public void ClearCredentials()
        //    {
        //        Settings.AptkAmsIdentityProvider = AptkAmsAuthenticationProvider.None;
        //        Settings.AptkAmsIdentityUserId = string.Empty;
        //        Settings.AptkAmsIdentityAuthToken = string.Empty;
        //    }
        //}
    }
}
