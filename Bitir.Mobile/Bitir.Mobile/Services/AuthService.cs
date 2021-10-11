using AuthModule.Dto;
using AuthModule.Security.JWT;
using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private const string loginCheckPath = "AuthModule/AuthBase/Login";
        private const string accountTypesPath = "AuthModule/AccountType/GetAll";
        private const string registerTypesPath = "AuthModule/AuthBase/Register";
        private const string getAccountDetailPath = "AuthModule/AuthBase/GetById";
        private const string updateAccountPath = "AuthModule/AuthBase/Update";
        public async Task<ResponseWrapper<AccessToken>> LoginCheckAsync(LoginDto request)
        {
            var restClientRequest = await GetRestClient(Method.POST, loginCheckPath);
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<AccessToken>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<AccessToken>> RegisterAsync(AuthRegisterRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, registerTypesPath);
            var jsonRequest = JsonConvert.SerializeObject(request);
            restClientRequest.Item2.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<AccessToken>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);

        }

        public async Task<ResponseWrapper<ProfileResponse>> GetAccountByIdAsync(int Id)
        {
            var restClientRequest = await GetRestClient(Method.GET, getAccountDetailPath);
            restClientRequest.Item2.AddParameter("Id", Id);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<ProfileResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);

        }

        public async Task<ResponseWrapper<ProfileResponse>> UpdateAccountAsync(AuthRegisterRequest request)
        {
            var restClientRequest = await GetRestClient(Method.PUT, updateAccountPath);
            var jsonRequest = JsonConvert.SerializeObject(request);
            restClientRequest.Item2.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<ProfileResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);

        }



    }
}
