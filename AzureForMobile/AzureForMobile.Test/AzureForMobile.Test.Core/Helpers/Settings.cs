// Helpers/Settings.cs

using Aptk.Plugins.AzureForMobile.Identity;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AzureForMobile.Test.Core.Helpers
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

        private const string AzureForMobileIdentityUserIdKey = "AzureForMobileIdentityUserId";
        private static readonly string AzureForMobileIdentityUserIdDefault = string.Empty;
        private const string AzureForMobileIdentityAuthTokenKey = "AzureForMobileIdentityAuthToken";
        private static readonly string AzureForMobileIdentityAuthTokenDefault = string.Empty;
        private const string AzureForMobileIdentityProviderKey = "AzureForMobileIdentityProvider";
        private static readonly AzureForMobileAuthenticationProvider AzureForMobileIdentityProviderDefault = AzureForMobileAuthenticationProvider.None;

        #endregion


        public static string AzureForMobileIdentityUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(AzureForMobileIdentityUserIdKey, AzureForMobileIdentityUserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AzureForMobileIdentityUserIdKey, value);
            }
        }

        public static string AzureForMobileIdentityAuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AzureForMobileIdentityAuthTokenKey, AzureForMobileIdentityAuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AzureForMobileIdentityAuthTokenKey, value);
            }
        }

        public static AzureForMobileAuthenticationProvider AzureForMobileIdentityProvider
        {
            get
            {
                return AppSettings.GetValueOrDefault(AzureForMobileIdentityProviderKey, AzureForMobileIdentityProviderDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AzureForMobileIdentityProviderKey, value);
            }
        }
    }
}