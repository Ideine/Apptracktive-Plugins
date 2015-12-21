using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Aptk.Plugins.AzureForMobile.Identity
{
    public abstract class AzureForMobileBaseIdentityService : IAzureForMobileIdentityService
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;
        private readonly IMobileServiceClient _client;

        protected AzureForMobileBaseIdentityService(IAzureForMobilePluginConfiguration configuration, IMobileServiceClient client)
        {
            _configuration = configuration;
            _client = client;
        }
        
        public MobileServiceUser CurrentUser
        {
            get { return _client.CurrentUser; }
            set { _client.CurrentUser = value; }
        }
        
        public abstract Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, IDictionary<string, string> parameters = null, bool useSingleSignOnIfAvailable = false);

        public async Task<MobileServiceUser> LoginAsync(AzureForMobileAuthenticationProvider provider, JObject token)
        {
            if (provider == AzureForMobileAuthenticationProvider.None || provider == AzureForMobileAuthenticationProvider.LoginPassword)
                throw new ArgumentException("AzureForMobileAuthenticationProvider must be of type MicrosoftAccount, Google, Twitter, Facebook or WindowsAzureActiveDirectory");

            var user = await _client.LoginAsync(provider.ToMobileServiceAuthenticationProvider(), token);

            _configuration.CredentialsCacheService?.SaveCredentials(new AzureForMobileCredentials(provider, user));

            return user;
        }

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

        public async Task<bool> EnsureLoggedInAsync(bool controlToken)
        {
            if (_client.CurrentUser != null)
            {
                if (controlToken)
                {
                    return await ControlToken(_client.CurrentUser.MobileServiceAuthenticationToken);
                }
                return true;
            }

            IAzureForMobileCredentials credentials;
            if (_configuration.CredentialsCacheService != null && _configuration.CredentialsCacheService.TryLoadCredentials(out credentials))
            {
                _client.CurrentUser = credentials.User;
                if (controlToken)
                {
                    return await ControlToken(_client.CurrentUser.MobileServiceAuthenticationToken);
                }
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _client.Logout();
        }

        private async Task<bool> ControlToken(string token)
        {
            var tokenObject = new JObject
            {
                { "Token", token }
            };
            
            return await _client.InvokeApiAsync<JObject, bool>("ControlToken", tokenObject);
        }
    }
}
