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
        private const string orderUpdateServicePath= "SalesModule/Order/Update";

        public async Task<ResponseWrapperListing<StoreOrdersView>> GetStoreOrders(PagingRequestEntity<StoreOrdersView> request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addCarrierToStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreOrdersView>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<Order>> StoreOrderUpdate(Order request)
        {
            var restClientRequest = await GetRestClient(Method.PUT, orderUpdateServicePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<Order>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }
    }
}
