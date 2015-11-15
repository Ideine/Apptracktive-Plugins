// Helpers/Settings.cs

using Aptk.Plugins.AzureMobileServices.Identity;
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace AptkAms.Sample.Core.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string AptkAmsIdentityUserIdKey = "AptkAmsIdentityUserId";
        private static readonly string AptkAmsIdentityUserIdDefault = string.Empty;
        private const string AptkAmsIdentityAuthTokenKey = "AptkAmsIdentityAuthToken";
        private static readonly string AptkAmsIdentityAuthTokenDefault = string.Empty;
        private const string AptkAmsIdentityProviderKey = "AptkAmsIdentityProvider";
        private static readonly AptkAmsAuthenticationProvider AptkAmsIdentityProviderDefault = AptkAmsAuthenticationProvider.None;

        #endregion


        public static string AptkAmsIdentityUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(AptkAmsIdentityUserIdKey, AptkAmsIdentityUserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AptkAmsIdentityUserIdKey, value);
            }
        }

        public static string AptkAmsIdentityAuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AptkAmsIdentityAuthTokenKey, AptkAmsIdentityAuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AptkAmsIdentityAuthTokenKey, value);
            }
        }

        public static AptkAmsAuthenticationProvider AptkAmsIdentityProvider
        {
            get
            {
                return AppSettings.GetValueOrDefault(AptkAmsIdentityProviderKey, AptkAmsIdentityProviderDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AptkAmsIdentityProviderKey, value);
            }
        }
    }
}