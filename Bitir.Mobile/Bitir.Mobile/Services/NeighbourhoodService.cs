using Bitir.Mobile.Services.Interfaces;
using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class NeighbourhoodService : BaseService, INeighbourhoodService
    {
        private const string getNeighbourhoodPath = "AuthModule/Neighbourhood/GetAll";

        public async Task<ResponseWrapperListing<Neighbourhood>> GetNeighbourhood(Neighbourhood request)
        {
            var restClientRequest = await GetRestClient(Method.POST, getNeighbourhoodPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<Neighbourhood>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
