using Bitir.Mobile.Services.Interfaces;
using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class DistrictService : BaseService, IDistrictService
    {
        private const string getDistrictPath = "AuthModule/District/GetAll";

        public async Task<ResponseWrapperListing<District>> GetDistrict(District request)
        {
            var restClientRequest = await GetRestClient(Method.POST, getDistrictPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<District>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
