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
        /// Logs a user server side into Azure
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="parameters">Optional identity provider specific extra parameters</param>
        /// <param name="useSingleSignOnIfAvailable">Use single sign on if available on platform</param>
        /// <returns>An authenticated Azure user</returns>
        Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false);

        /// <summary>
        /// Logs a user client side into Azure
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="token">Identity provider authentication token</param>
        /// <returns>An authenticated Azure user</returns>
        Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, JObject token);

        /// <summary>
        /// Logs a user into Azure
        /// This request requires you to create a CustomLogin api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>An authenticated Azure user</returns>
        Task<MobileServiceUser> LoginAsync(string login, string password);

        /// <summary>
        /// Register a user into Azure
        /// This request requires you to create a CustomRegistration api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <param name="userInfos">Optional user registration informations</param>
        /// <returns>An authenticated Azure user</returns>
        Task<MobileServiceUser> RegisterAsync(string login, string password, JObject userInfos = null);

        /// <summary>
        /// Retrieve identity provider authentication token when logged in for further use
        /// This request requires you to create a CustomToken api contoller
        /// </summary>
        /// <returns>Identity provider authentication token</returns>
        Task<string> GetIdentityProviderTokenAsync();

        /// <summary>
        /// Check if user is logged in or silent logs in with stored credentials (if exist)
        /// </summary>
        /// <returns>True if logged in</returns>
        bool EnsureLoggedIn();

        /// <summary>
        /// Logs a user out from Azure and clear cache (if exist)
        /// </summary>
        void Logout();
    }
}
