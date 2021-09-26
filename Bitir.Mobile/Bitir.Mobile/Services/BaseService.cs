using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bitir.Mobile.Services
{
    public abstract class BaseService : HttpClient, IDisposable
    {
        private const string rootUrl = "http://192.168.1.73";
        private const int port = 45455;
        public readonly string apiUrl = $"{rootUrl}:{port}";

        protected HttpClient Client;
        protected static object lockObj = new object();

        public virtual async Task<HttpClient> GetClient()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new Exception("İnternet bağlantınızı kontrol edin");

            var current = Plugin.Connectivity.CrossConnectivity.Current;
            if (current.IsConnected)
            {
                var IsServiceOn = await current.IsRemoteReachable(apiUrl);
                if (!IsServiceOn)
                    throw new Exception("Servise erişilemiyor");
            }

            lock (lockObj)
            {
                if (Client == null)
                {
                    Client = new HttpClient();
                    Client.BaseAddress = new Uri(apiUrl);
                    if (App.authResponse != null)
                        Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.authResponse.Token);
                    Client.DefaultRequestHeaders.Add("accept", "Applciation/json");
                }
            }

            return await Task.FromResult(Client);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
