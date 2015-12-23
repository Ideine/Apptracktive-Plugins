using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    /// <summary>
    /// Service to manage identity
    /// </summary>
    public abstract class AzureForMobileBaseIdentityService : IAzureForMobileIdentityService
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;
        
        protected AzureForMobileBaseIdentityService(IAzureForMobilePluginConfiguration configuration, IMobileServiceClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        /// <summary>
        /// The current authenticated user provided after a successful call to LoginAsync or could be provided manually
        /// </summary>
        public MobileServiceUser CurrentUser
        {
            get { return _client.CurrentUser; }
            set { _client.CurrentUser = value; }
        }

        /// <summary>
        /// Logs a user server side into Azure
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="parameters">Optional identity provider specific extra parameters</param>
        /// <param name="useSingleSignOnIfAvailable">Use single sign on if available on platform</param>
        /// <returns>An authenticated Azure user</returns>
        public abstract Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false);

        /// <summary>
        /// Logs a user client side into Azure
        /// </summary>
        /// <param name="provider">Identity provider to log with (must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory)</param>
        /// <param name="token">Identity provider authentication token</param>
        /// <returns>An authenticated Azure user</returns>
        public async Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, JObject token)
        {
            if (provider == AzureForMobileAuthenticationProvider.None || provider == AzureForMobileAuthenticationProvider.LoginPassword)
                throw new ArgumentException("AzureForMobileAuthenticationProvider must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory");

            var user = await _client.LoginAsync(provider.ToMobileServiceAuthenticationProvider(), token);

            _configuration.CredentialsCacheService?.SaveCredentials(new AzureForMobileCredentials(provider, user));

            return user;
        }

        /// <summary>
        /// Logs a user into Azure
        /// This request requires you to create a CustomLogin api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>An authenticated Azure user</returns>
        public async Task<MobileServiceUser> LoginAsync(string login, string password)
        {
            var registrationInfos = new JObject
            {
                {"login", login},
                {"password", password}
            };
            
            var user = await _client.InvokeApiAsync<JObject, MobileServiceUser>("CustomLogin", registrationInfos);
            
            _configuration.CredentialsCacheService?.SaveCredentials(new AzureForMobileCredentials(AzureForMobileAuthenticationProvider.LoginPassword, user));

            return user;
        }

        /// <summary>
        /// Register a user into Azure
        /// This request requires you to create a CustomRegistration api contoller
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <param name="userInfos">Optional user registration informations</param>
        /// <returns>An authenticated Azure user</returns>
        public async Task<MobileServiceUser> RegisterAsync(string login, string password, JObject userInfos = null)
        {
            var registration = new JObject
            {
                {"login", login},
                {"password", password},
                userInfos
            };
            
            var user = await _client.InvokeApiAsync<JObject, MobileServiceUser>("CustomRegistration", registration);
            
            _configuration.CredentialsCacheService?.SaveCredentials(new AzureForMobileCredentials(AzureForMobileAuthenticationProvider.LoginPassword, user));

            return user;
        }

        /// <summary>
        /// Retrieve social authentication token when logged in for further use
        /// This request requires you to create a CustomToken api contoller
        /// </summary>
        /// <returns>Identity provider authentication token</returns>
        public async Task<string> GetIdentityProviderTokenAsync()
        {
            return await _client.InvokeApiAsync<string>("CustomToken");
        }

        /// <summary>
        /// Check if user is logged in or silent logs in with stored credentials (if exist)
        /// </summary>
        /// <returns>True if logged in</returns>
        public bool EnsureLoggedIn()
        {
            if (_client.CurrentUser != null)
            {
                return ControlToken(_client.CurrentUser.MobileServiceAuthenticationToken);
            }

            IAzureForMobileCredentials credentials;
            if (_configuration.CredentialsCacheService != null && _configuration.CredentialsCacheService.TryLoadCredentials(out credentials))
            {
                _client.CurrentUser = credentials.User;

                return ControlToken(_client.CurrentUser.MobileServiceAuthenticationToken);
            }

            return false;
        }

        /// <summary>
        /// Logs a user out from Azure and clear cache (if exist)
        /// </summary>
        public void Logout()
        {
            _client.Logout();
            _configuration.CredentialsCacheService?.ClearCredentials();
        }

        /// <summary>
        /// Control token expiration date
        /// Code from Glenn Gailey
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool ControlToken(string token)
        {
            // Check for a signed-in user.
            if (string.IsNullOrEmpty(token))
            {
                // Raise an exception if there is no token.
                throw new InvalidOperationException(
                    "The client isn't signed-in or the token value isn't set.");
            }

            // Get just the JWT part of the token.
            var jwt = token.Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+');
            jwt = jwt.Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new System.Exception(
               "The base64url string is not valid.");
            }

            // Decode the bytes from base64 and write to a JSON string.
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value, 
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the 
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);

            // If the expiration date is less than now, the token is expired and we return true.
            return expire < DateTime.UtcNow;
        }
    }
}
