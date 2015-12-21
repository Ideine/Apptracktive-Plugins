using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public static class AzureForMobileAuthenticationProviderExtension
    {
        public static MobileServiceAuthenticationProvider ToMobileServiceAuthenticationProvider(
            this AzureForMobileAuthenticationProvider authenticationProvider)
        {
            switch (authenticationProvider)
            {
                default:
                    return MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;
                case AzureForMobileAuthenticationProvider.MicrosoftAccount:
                    return MobileServiceAuthenticationProvider.MicrosoftAccount;
                case AzureForMobileAuthenticationProvider.Google:
                    return MobileServiceAuthenticationProvider.Google;
                case AzureForMobileAuthenticationProvider.Twitter:
                    return MobileServiceAuthenticationProvider.Twitter;
                case AzureForMobileAuthenticationProvider.Facebook:
                    return MobileServiceAuthenticationProvider.Facebook;
            }
        }
    }
}
