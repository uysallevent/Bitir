using AuthModule.Dto;
using AuthModule.Security.JWT;
using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Core.Wrappers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bitir.Mobile.Services
{
    public abstract class BaseService : RestClient
    {
        private const string refreshLoginPath = "AuthModule/AuthBase/RefreshTokenLogin";
        private const string rootUrl = "192.168.1.40";
        private const int port = 45455;
        protected UriBuilder uriBuilder;

        public virtual async Task<(RestClient, RestRequest)> GetRestClient(Method method, string path = null)
        {
            uriBuilder = new UriBuilder();
            uriBuilder.Host = rootUrl;
            uriBuilder.Port = port;

            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                throw new ServiceException("İnternet bağlantınızı kontrol edin");

            var current = Plugin.Connectivity.CrossConnectivity.Current;
            if (current.IsConnected)
            {
                var IsServiceOn = await current.IsRemoteReachable(uriBuilder.Uri.AbsoluteUri);
                if (!IsServiceOn)
                    throw new ServiceException("Servise erişilemiyor");
            }
            await CheckTokenExpiration(path);
            return RestClient(method, path);
        }

        private (RestClient, RestRequest) RestClient(Method method, string path)
        {

            var restClient = new RestClient(uriBuilder.Uri);
            restClient.Timeout = 60000;

            RestRequest restRequest = new RestRequest(path, method);
            restRequest.AddHeader("Authorization", $"Bearer {App.authResponse?.Token}");
            restRequest.AddHeader("RefreshToken", App.authResponse?.RefreshToken);
            restRequest.AddHeader("Accept", "Application/json");
            return (restClient, restRequest);
        }

        public async Task CheckTokenExpiration(string path = null)
        {
            if (path != "AuthModule/AuthBase/Login" && App.authResponse?.Expiration < DateTime.Now)
            {
                var restClientRequest = RestClient(Method.POST, refreshLoginPath);
                restClientRequest.Item2.AddJsonBody(new LoginDto
                {
                    RefreshToken = App.authResponse.RefreshToken
                });
                var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<AccessToken>>(restClientRequest.Item2);
                var refreshLoginResult = ResponseHandler(restResponse);
                App.authResponse = refreshLoginResult.Result;
            }
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
            else if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new BadRequestException("Lütfen tekrar giriş yapın");
            }
            else
            {
                throw new InternalServerErrorException("Servis hatası");
            }

        }
    }
}
