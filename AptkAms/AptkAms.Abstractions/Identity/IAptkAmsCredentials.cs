using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public interface IAptkAmsCredentials
    {
        /// <summary>
        /// Identity provider for this User
        /// </summary>
        AptkAmsAuthenticationProvider Provider { get; }

        /// <summary>
        /// Mobile service user
        /// </summary>
        MobileServiceUser User { get; }
    }
}
