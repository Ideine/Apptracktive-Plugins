using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    /// <summary>
    /// Service to manage identity
    /// </summary>
    public interface IAzureForMobileIdentityService
    {
        /// <summary>
        /// The current authenticated user provided after a successful call to LoginAsync or could be provided manually
        /// </summary>
        MobileServiceUser CurrentUser { get; set; }

        /// <summary>
        /// Logs a user server side into Azure Mobile Services
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="parameters">Optional identity provider specific extra parameters</param>
        /// <param name="useSingleSignOnIfAvailable">Use single sign on if available on platform</param>
        /// <returns>An authenticated Azure Mobile Services user</returns>
        Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false);

        /// <summary>
        /// Logs a user client side into Azure Mobile Services
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="token">Identity provider authentication token</param>
        /// <returns>An authenticated Azure Mobile Services user</returns>
        Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, JObject token);

        /// <summary>
        /// Logs a user into Azure Mobile Services
        /// This request requires you to create a custom login api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        Task<MobileServiceUser> LoginAsync(string login, string password);

        /// <summary>
        /// Register a user into Azure Mobile Services
        /// This request requires you to create a custom registration api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <param name="userInfos">Optional user registration informations</param>
        /// <returns></returns>
        Task<MobileServiceUser> RegisterAsync(string login, string password, JObject userInfos = null);

        /// <summary>
        /// Check if user is logged in or silent logs in with stored credentials (if exist)
        /// Setting controlToken to true requires you to create an ControlToken custom api contoller
        /// </summary>
        /// <returns></returns>
        Task<bool> EnsureLoggedInAsync(bool controlToken);

        /// <summary>
        /// Logs a user out from Azure Mobile Services
        /// </summary>
        void Logout();
    }
}
