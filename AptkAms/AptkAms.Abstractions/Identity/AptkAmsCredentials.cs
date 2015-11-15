using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsCredentials : IAptkAmsCredentials
    {
        public AptkAmsCredentials(AptkAmsAuthenticationProvider provider, MobileServiceUser user)
        {
            Provider = provider;
            User = user;
        }

        /// <summary>
        /// Identity provider for this User
        /// </summary>
        public AptkAmsAuthenticationProvider Provider { get; }

        /// <summary>
        /// Mobile service user
        /// </summary>
        public MobileServiceUser User { get; }
    }
}
