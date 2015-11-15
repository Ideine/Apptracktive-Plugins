﻿using System.Reflection;
using AptkAms.Sample.Core.Model;

namespace AptkAms.Sample.Core
{
    public static class Constants
    {
        public static readonly string AmsAppUrl = "YOUR URL HERE";

        public static readonly string AmsAppKey = "YOUR KEY HERE";

        public static readonly Assembly ModelAssembly = typeof(TodoItem).GetTypeInfo().Assembly;
    }
}
