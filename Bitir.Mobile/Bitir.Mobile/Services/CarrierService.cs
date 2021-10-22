using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Services.Interfaces;
using RestSharp;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class CarrierService : BaseService, ICarrierService
    {
        private const string addCarrierToStorePath = "SalesModule/Carrier/AddCarrierToStore";
        private const string UpdateStoreCarrierPath = "SalesModule/Carrier/UpdateStoreCarrier";
        private const string storeCarriersPath = "SalesModule/Carrier/GetStoreCarriers";

        public async Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, UpdateStoreCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreCarrier>> GetStoreCarriers()
        {
            var restClientRequest = await GetRestClient(Method.POST, storeCarriersPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreCarrier>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
