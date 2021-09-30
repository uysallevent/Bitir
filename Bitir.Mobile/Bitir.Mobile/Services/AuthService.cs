using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Exeptions;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            ResponseWrapper<AuthResponse> result = null;
            var httpClient = await GetClient();
            var response = await httpClient.PostAsync(loginCheckPath, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringInResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseWrapper<AuthResponse>>(stringInResponse);
            }
            else
            {
                var stringInResponse = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
                throw new ServiceException($"Servis hatası !! | {stringInResponse}");
            }
            Dispose();
            return result;
        }

        public async Task<ResponseWrapperListing<AccountTypeResponse>> GetAccoutTypesAsync()
        {
            ResponseWrapperListing<AccountTypeResponse> result = null;
            var httpClient = await GetClient();
            var response = await httpClient.PostAsync(accountTypesPath, new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringInResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseWrapperListing<AccountTypeResponse>>(stringInResponse);
            }
            else
            {
                var stringInResponse = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
                throw new ServiceException($"Servis hatası !! | {stringInResponse}");
            }

            Dispose();
            return result;
        }

        public async Task<ResponseWrapper<AuthResponse>> RegisterAsync(AuthRegisterRequest request)
        {
            ResponseWrapper<AuthResponse> result = null;
            var httpClient = await GetClient();
            var response = await httpClient.PostAsync(registerTypesPath, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringInResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseWrapper<AuthResponse>>(stringInResponse);
            }
            else
            {
                var stringInResponse = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
                throw new ServiceException($"Servis hatası !! | {stringInResponse}");
            }

            Dispose();
            return result;
        }

        public async Task<ResponseWrapper<ProfileResponse>> GetAccountByIdAsync(int Id)
        {
            ResponseWrapper<ProfileResponse> result = null;
            var httpClient = await GetClient(getAccountDetailPath);
            var paramCollection = new NameValueCollection();
            paramCollection.Add("Id", Id.ToString());
            var response = await httpClient.GetAsync(QueryParamBuilder(paramCollection));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringInResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseWrapper<ProfileResponse>>(stringInResponse);
            }
            else
            {
                var stringInResponse = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
                throw new ServiceException($"Servis hatası !! | {stringInResponse}");
            }

            Dispose();
            return result;
        }

        public async Task<ResponseWrapper<ProfileResponse>> UpdateAccountAsync(AuthRegisterRequest request)
        {
            ResponseWrapper<ProfileResponse> result = null;
            var httpClient = await GetClient();
            var response = await httpClient.PutAsync(updateAccountPath, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringInResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseWrapper<ProfileResponse>>(stringInResponse);
            }
            else
            {
                var stringInResponse = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
                throw new ServiceException($"Servis hatası !! | {stringInResponse}");
            }

            Dispose();
            return result;
        }

    }
}
