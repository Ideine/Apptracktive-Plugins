using Aptk.Plugins.AzureForMobile.Identity;
using AzureForMobile.Test.Core.Helpers;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureForMobile.Test.Core.Services
{
    /// <summary>
    /// This IAzureForMobileCredentialsCacheService implementation is a working example 
    /// requiring the installation of Xamarin Settings plugin.
    /// </summary>
    public class AzureForMobileCredentialCacheService : IAzureForMobileCredentialsCacheService
    {
        public bool TryLoadCredentials(out IAzureForMobileCredentials credentials)
        {
            credentials = !string.IsNullOrEmpty(Settings.AzureForMobileIdentityUserId)
                          && !string.IsNullOrEmpty(Settings.AzureForMobileIdentityAuthToken)
                          && Settings.AzureForMobileIdentityProvider != AzureForMobileAuthenticationProvider.None
                ? new AzureForMobileCredentials(Settings.AzureForMobileIdentityProvider, new MobileServiceUser(Settings.AzureForMobileIdentityUserId)
                    {
                        MobileServiceAuthenticationToken = Settings.AzureForMobileIdentityAuthToken
                    })
                : null;

            return credentials != null;
        }

        public void SaveCredentials(IAzureForMobileCredentials credentials)
        {
            if (credentials == null)
                return;

            Settings.AzureForMobileIdentityProvider = credentials.Provider;
            Settings.AzureForMobileIdentityUserId = credentials.User.UserId;
            Settings.AzureForMobileIdentityAuthToken = credentials.User.MobileServiceAuthenticationToken;
        }

        public void ClearCredentials()
        {
            Settings.AzureForMobileIdentityProvider = AzureForMobileAuthenticationProvider.None;
            Settings.AzureForMobileIdentityUserId = string.Empty;
            Settings.AzureForMobileIdentityAuthToken = string.Empty;
        }
    }
}
