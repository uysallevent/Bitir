using Core.Wrappers;
using Bitir.Mobile.Services.Interfaces;
using RestSharp;
using SalesModule.Dtos;
using System.Threading.Tasks;
using Module.Shared.Entities.SalesModuleEntities;

namespace Bitir.Mobile.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private const string addCarrierToStorePath = "SalesModule/Order/GetStoreOrders";

        public async Task<ResponseWrapperListing<StoreOrderViewModel>> GetStoreOrders()
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreOrderViewModel>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
