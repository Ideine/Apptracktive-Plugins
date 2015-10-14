using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Aptk.Plugins.AzureMobileServices.Api
{
    public class AptkAmsApiService : IAptkAmsApiService
    {
        public async Task<T> InvokeApiAsync<T>(string apiName)
        {
            return await Loader.ClientInstance.InvokeApiAsync<T>(apiName);
        }

        public async Task<U> InvokeApiAsync<T, U>(string apiName, T body)
        {
            return await Loader.ClientInstance.InvokeApiAsync<T, U>(apiName, body);
        }

        public async Task<T> InvokeApiAsync<T>(string apiName, HttpMethod method, IDictionary<string, string> parameters)
        {
            return await Loader.ClientInstance.InvokeApiAsync<T>(apiName, method, parameters);
        }

        public async Task<U> InvokeApiAsync<T, U>(string apiName, T body, HttpMethod method, IDictionary<string, string> parameters)
        {
            return await Loader.ClientInstance.InvokeApiAsync<T, U>(apiName, body, method, parameters);
        }

        public async Task<HttpResponseMessage> InvokeApiAsync(string apiName, HttpContent content, HttpMethod method, IDictionary<string, string> requestHeaders,
            IDictionary<string, string> parameters)
        {
            return await Loader.ClientInstance.InvokeApiAsync(apiName, content, method, requestHeaders, parameters);
        }
    }
}
