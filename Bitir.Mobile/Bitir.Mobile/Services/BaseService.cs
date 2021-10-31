using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace Bitir.Mobile.Services
{
    public abstract class BaseService : RestClient
    {
        private const string rootUrl = "192.168.1.73";
        private const int port = 45455;
        protected RestClient restClient;
        protected UriBuilder uriBuilder;

        public virtual async Task<(RestClient, RestRequest)> GetRestClient(Method method, string path = null)
        {
            uriBuilder = new UriBuilder();
            uriBuilder.Host = rootUrl;
            uriBuilder.Port = port;
            if (restClient == null)
            {
                restClient = new RestClient(uriBuilder.Uri);
                restClient.Timeout = 60000;
            }
            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                throw new ServiceException("İnternet bağlantınızı kontrol edin");

            var current = Plugin.Connectivity.CrossConnectivity.Current;
            if (current.IsConnected)
            {
                var IsServiceOn = await current.IsRemoteReachable(uriBuilder.Uri.AbsoluteUri);
                if (!IsServiceOn)
                    throw new ServiceException("Servise erişilemiyor");
            }
            RestRequest restRequest = new RestRequest(path, method);
            restRequest.AddHeader("Authorization", $"Bearer {App.authResponse?.Token}");
            restRequest.AddHeader("Accept", "Application/json");
            return (restClient, restRequest);
        }

        protected virtual T ResponseHandler<T>(IRestResponse<T> restResponse)
        {
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return restResponse.Data;
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException(JsonConvert.DeserializeObject<ErrorResponse>(restResponse.Content));
            }
            else
            {
                throw new InternalServerErrorException("Servis hatası");
            }

        }
    }
}
