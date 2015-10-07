using System;
using Aptk.Plugins.AzureMobileServices.Abstractions;

namespace Aptk.Plugins.AzureMobileServices
{
    /// <summary>
    /// Cross platform AzureMobileServices implemenations
    /// </summary>
    public class CrossAzureMobileServices
  {
    static Lazy<IAzureMobileServices> Implementation = new Lazy<IAzureMobileServices>(() => CreateAzureMobileServices(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IAzureMobileServices Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IAzureMobileServices CreateAzureMobileServices()
    {
#if PORTABLE
        return null;
#else
        return new AzureMobileServicesImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
