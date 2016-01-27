using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace AzureForMobile.Sample.Backend.Utils
{
    public class CustomLoginProviderCredentials : ProviderCredentials
    {
        public CustomLoginProviderCredentials()
            : base(CustomLoginProvider.ProviderName)
        {
        }
    }
}