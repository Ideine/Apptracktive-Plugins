using System.Reflection;
using AzureForMobile.Test.Core.Model;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureForMobile.Test.Core
{
    public static class Constants
    {
        public static readonly string AmsUrl = "YOUR URL";

        public static readonly string AmsKey = "YOUR KEY";

        public static readonly Assembly ModelAssembly = typeof(TodoItem).GetTypeInfo().Assembly;
    }
}
