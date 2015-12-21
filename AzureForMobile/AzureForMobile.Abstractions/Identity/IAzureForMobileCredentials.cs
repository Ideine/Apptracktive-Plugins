using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public interface IAzureForMobileCredentials
    {
        /// <summary>
        /// Identity provider for this User
        /// </summary>
        AzureForMobileAuthenticationProvider Provider { get; }

        /// <summary>
        /// Mobile service user
        /// </summary>
        MobileServiceUser User { get; }
    }
}
