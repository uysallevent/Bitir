using Core.Wrappers;
using Bitir.Mobile.Services.Interfaces;
using RestSharp;
using SalesModule.Dtos;
using System.Threading.Tasks;
using Module.Shared.Entities.SalesModuleEntities;

namespace Bitir.Mobile.Services
{
    public class CarrierService : BaseService, ICarrierService
    {
        private const string addCarrierToStorePath = "SalesModule/Carrier/AddCarrierToStore";
        private const string AddDistributionZoneToCarrierPath = "SalesModule/Carrier/AddDistributionZoneToCarrier";
        private const string UpdateStoreCarrierPath = "SalesModule/Carrier/UpdateStoreCarrier";
        private const string storeCarriersPath = "SalesModule/Carrier/GetStoreCarriers";
        private const string storeCarrierByIdPath = "SalesModule/Carrier/GetStoreCarriersByCarrierId";
        private const string RemoveZoneFromCarrierByIdPath = "SalesModule/Carrier/RemoveZoneFromCarrierById";

        public async Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> AddDistributionZoneToCarrier(CarrierZoneRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, AddDistributionZoneToCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.PUT, UpdateStoreCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarriers()
        {
            var restClientRequest = await GetRestClient(Method.POST, storeCarriersPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreCarriersView>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarrierById(int carrierId)
        {
            var restClientRequest = await GetRestClient(Method.POST, storeCarrierByIdPath);
            restClientRequest.Item2.AddQueryParameter("carrierId", carrierId.ToString());
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreCarriersView>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> RemoveZoneFromCarrierById(int carrierDistributionZoneId)
        {
            var restClientRequest = await GetRestClient(Method.DELETE, RemoveZoneFromCarrierByIdPath);
            restClientRequest.Item2.AddQueryParameter("carrierDistributionZoneId", carrierDistributionZoneId.ToString());
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
