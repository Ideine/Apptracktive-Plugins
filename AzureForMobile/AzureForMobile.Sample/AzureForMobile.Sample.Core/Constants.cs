using System.Reflection;
using AzureForMobile.Sample.Core.Model;

namespace AzureForMobile.Sample.Core
{
    public static class Constants
    {
        public static readonly string AmsUrl = "YOUR URL";

        public static readonly string AmsKey = "YOUR KEY";

        public static readonly Assembly ModelAssembly = typeof(TodoItem).GetTypeInfo().Assembly;
    }
}
