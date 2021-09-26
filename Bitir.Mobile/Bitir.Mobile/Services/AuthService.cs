using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Exeptions;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private const string loginCheckPath = "AuthModule/AuthBase/Login";
        public async Task<ResponseWrapper<AuthResponse>> LoginCheck(AuthRequest request)
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
    }
}
