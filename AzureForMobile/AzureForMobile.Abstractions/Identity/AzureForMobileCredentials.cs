using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public class AzureForMobileCredentials : IAzureForMobileCredentials
    {
        public AzureForMobileCredentials(AzureForMobileAuthenticationProvider provider, MobileServiceUser user)
        {
            Provider = provider;
            User = user;
        }

        /// <summary>
        /// Identity provider for this User
        /// </summary>
        public AzureForMobileAuthenticationProvider Provider { get; }

        /// <summary>
        /// Mobile service user
        /// </summary>
        public MobileServiceUser User { get; }
    }
}
