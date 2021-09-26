using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public class AuthService : BaseService, IAuthService
    {
        private const string loginCheckPath = "";
        public async Task<bool> LoginCheck()
        {
            var httpClient = await GetClient();
            var response = await httpClient.PostAsync(loginCheckPath, new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(true);
        }
    }
}
