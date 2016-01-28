using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureForMobile.Sample.Backend.DataObjects
{
    public class RegistrationRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}