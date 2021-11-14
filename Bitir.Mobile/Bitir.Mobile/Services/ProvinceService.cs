using Bitir.Mobile.Services.Interfaces;
using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class ProvinceService : BaseService, IProvinceService
    {
        private const string getProvincePath = "AuthModule/Province/GetAll";

        public async Task<ResponseWrapper<Province>> GetProvince(Province request)
        {
            var restClientRequest = await GetRestClient(Method.POST, getProvincePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<Province>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
