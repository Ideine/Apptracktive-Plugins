using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public static class AptkAmsAuthenticationProviderExtension
    {
        public static MobileServiceAuthenticationProvider ToMobileServiceAuthenticationProvider(
            this AptkAmsAuthenticationProvider authenticationProvider)
        {
            switch (authenticationProvider)
            {
                default:
                    return MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;
                case AptkAmsAuthenticationProvider.MicrosoftAccount:
                    return MobileServiceAuthenticationProvider.MicrosoftAccount;
                case AptkAmsAuthenticationProvider.Google:
                    return MobileServiceAuthenticationProvider.Google;
                case AptkAmsAuthenticationProvider.Twitter:
                    return MobileServiceAuthenticationProvider.Twitter;
                case AptkAmsAuthenticationProvider.Facebook:
                    return MobileServiceAuthenticationProvider.Facebook;
            }
        }
    }
}
