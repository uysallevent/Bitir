using Bitir.Mobile.Exeptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace Bitir.Mobile.Services
{
    public abstract class BaseService : HttpClient, IDisposable
    {
        private const string rootUrl = "192.168.1.73";
        private const int port = 45455;
        protected HttpClient Client;
        protected static object lockObj = new object();
        protected UriBuilder uriBuilder;

        public virtual async Task<HttpClient> GetClient(string path = null)
        {
            uriBuilder = new UriBuilder();
            uriBuilder.Host = rootUrl;
            uriBuilder.Port = port;
            uriBuilder.Path = path;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new ServiceException("İnternet bağlantınızı kontrol edin");

            var current = Plugin.Connectivity.CrossConnectivity.Current;
            if (current.IsConnected)
            {
                var IsServiceOn = await current.IsRemoteReachable(uriBuilder.Uri.AbsoluteUri);
                if (!IsServiceOn)
                    throw new ServiceException("Servise erişilemiyor");
            }

            if (Client == null)
            {
                Client = new HttpClient();
                Client.BaseAddress = uriBuilder.Uri;
            }

            Client.DefaultRequestHeaders.Authorization = (App.authResponse != null) ? new AuthenticationHeaderValue("Bearer", App.authResponse.Token) : null;
            Client.DefaultRequestHeaders.Add("accept", "Application/json");
            return await Task.FromResult(Client);
        }

        public string QueryParamBuilder(NameValueCollection parameters)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add(parameters);
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
