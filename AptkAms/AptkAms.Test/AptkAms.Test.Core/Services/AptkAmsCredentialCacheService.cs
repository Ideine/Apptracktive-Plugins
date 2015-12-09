using Aptk.Plugins.AzureMobileServices.Identity;
using AptkAms.Test.Core.Helpers;
using Microsoft.WindowsAzure.MobileServices;

namespace AptkAms.Test.Core.Services
{
    /// <summary>
    /// This IAptkAmsCredentialsCacheService implementation is a working example 
    /// requiring the installation of Xamarin Settings plugin.
    /// </summary>
    public class AptkAmsCredentialCacheService : IAptkAmsCredentialsCacheService
    {
        public bool TryLoadCredentials(out IAptkAmsCredentials credentials)
        {
            credentials = !string.IsNullOrEmpty(Settings.AptkAmsIdentityUserId)
                          && !string.IsNullOrEmpty(Settings.AptkAmsIdentityAuthToken)
                          && Settings.AptkAmsIdentityProvider != AptkAmsAuthenticationProvider.None
                ? new AptkAmsCredentials(Settings.AptkAmsIdentityProvider, new MobileServiceUser(Settings.AptkAmsIdentityUserId)
                    {
                        MobileServiceAuthenticationToken = Settings.AptkAmsIdentityAuthToken
                    })
                : null;

            return credentials != null;
        }

        public void SaveCredentials(IAptkAmsCredentials credentials)
        {
            if (credentials == null)
                return;

            Settings.AptkAmsIdentityProvider = credentials.Provider;
            Settings.AptkAmsIdentityUserId = credentials.User.UserId;
            Settings.AptkAmsIdentityAuthToken = credentials.User.MobileServiceAuthenticationToken;
        }

        public void ClearCredentials()
        {
            Settings.AptkAmsIdentityProvider = AptkAmsAuthenticationProvider.None;
            Settings.AptkAmsIdentityUserId = string.Empty;
            Settings.AptkAmsIdentityAuthToken = string.Empty;
        }
    }
}
