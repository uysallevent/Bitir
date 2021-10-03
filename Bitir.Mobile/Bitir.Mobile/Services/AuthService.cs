using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
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
        public async Task<ResponseWrapper<AuthResponse>> LoginCheckAsync(AuthLoginRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, loginCheckPath);
            var jsonRequest = JsonConvert.SerializeObject(request);
            restClientRequest.Item2.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<AuthResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<AccountTypeResponse>> GetAccoutTypesAsync()
        {
            var restClientRequest = await GetRestClient(Method.POST, accountTypesPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<AccountTypeResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);

        }

        public async Task<ResponseWrapper<AuthResponse>> RegisterAsync(AuthRegisterRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, registerTypesPath);
            var jsonRequest = JsonConvert.SerializeObject(request);
            restClientRequest.Item2.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<AuthResponse>>(restClientRequest.Item2);
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
