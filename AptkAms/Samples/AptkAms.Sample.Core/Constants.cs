using System.Reflection;
using AptkAms.Sample.Core.Model;

namespace AptkAms.Sample.Core
{
    public static class Constants
    {
        public static readonly string AmsAppUrl = "https://aptkamssamplebackend.azure-mobile.net/";

        public static readonly string AmsAppKey = "WweOcXBjVwkHbQYidZdkaJELdySxDy82";

        public static readonly Assembly ModelAssembly = typeof(TodoItem).GetTypeInfo().Assembly;
    }
}
