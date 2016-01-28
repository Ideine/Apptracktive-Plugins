﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace AzureForMobile.Sample.Backend.DataObjects
{
    public class Account : EntityData
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
    }
}