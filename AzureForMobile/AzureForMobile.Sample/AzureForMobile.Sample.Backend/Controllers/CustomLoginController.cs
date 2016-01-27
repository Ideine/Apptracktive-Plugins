using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AzureForMobile.Sample.Backend.DataObjects;
using AzureForMobile.Sample.Backend.Models;
using AzureForMobile.Sample.Backend.Utils;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Newtonsoft.Json.Linq;

namespace AzureForMobile.Sample.Backend.Controllers
{
    public class CustomLoginController : ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler Handler { get; set; }

        // POST api/CustomLogin
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Login) || string.IsNullOrEmpty(loginRequest.Password))
                return Request.CreateBadRequestResponse("Login and Password should not be null");
            
            var context = new MobileServiceContext();
            var account = context.Accounts.SingleOrDefault(a => a.Login == loginRequest.Login);
            if (account != null)
            {
                var incoming = CustomLoginProviderUtils.Hash(loginRequest.Password, account.Salt);

                if (CustomLoginProviderUtils.SlowEquals(incoming, account.SaltedAndHashedPassword))
                {
                    var claimsIdentity = new ClaimsIdentity();
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginRequest.Login));
                    var loginResult = new CustomLoginProvider(Handler).CreateLoginResult(claimsIdentity, Services.Settings.MasterKey);
                    var customLoginResult = new JObject
                    {
                        { "userId", loginResult.User.UserId },
                        { "mobileServiceAuthenticationToken", loginResult.AuthenticationToken }
                    };
                    return this.Request.CreateResponse(HttpStatusCode.OK, customLoginResult);
                }
            }
            return this.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid username or password");
        }
    }
}
