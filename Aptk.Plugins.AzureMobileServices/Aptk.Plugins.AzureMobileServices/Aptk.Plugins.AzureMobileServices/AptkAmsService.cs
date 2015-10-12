using Aptk.Plugins.AzureMobileServices.Api;
using Aptk.Plugins.AzureMobileServices.Data;
using Aptk.Plugins.AzureMobileServices.Identity;

namespace Aptk.Plugins.AzureMobileServices
{
  /// <summary>
  /// Implementation for Feature
  /// </summary>
  public class AptkAmsService : IAptkAmsService
  {
      public IAptkAmsDataService Data { get; }
      public IAptkAmsIdentityService Identity { get; }
      public IAptkAmsApiService Api { get; }
  }
}