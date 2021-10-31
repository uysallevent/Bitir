using Bitir.Mobile.Services.Interfaces;
using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.SalesModuleEntities;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private const string addCarrierToStorePath = "SalesModule/Order/GetStoreOrders";

        public async Task<ResponseWrapperListing<StoreOrderViewModel>> GetStoreOrders(PagingRequestEntity<StoreOrderViewModel> request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreOrderViewModel>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
