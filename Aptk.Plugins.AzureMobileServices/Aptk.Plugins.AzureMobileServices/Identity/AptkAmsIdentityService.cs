using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public class AptkAmsIdentityService : IAptkAmsIdentityService
    {
        public MobileServiceUser CurrentUser
        {
            get { return Loader.ClientInstance.CurrentUser; }
            set { Loader.ClientInstance.CurrentUser = value; }
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            return await Loader.PlatformIdentityInstance.LoginAsync(provider, parameters);
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, JObject token)
        {
            return await Loader.ClientInstance.LoginAsync(provider, token);
        }

        public void Logout()
        {
            Loader.ClientInstance.Logout();
        }
    }
}
