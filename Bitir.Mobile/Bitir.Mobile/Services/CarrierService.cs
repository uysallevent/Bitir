using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Services.Interfaces;
using RestSharp;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class CarrierService : BaseService, ICarrierService
    {
        private const string addCarrierToStorePath = "ProductModule/Product/AddProductToStore";

        public async Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody("");
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
