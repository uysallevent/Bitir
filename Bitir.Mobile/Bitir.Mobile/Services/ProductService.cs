using Bitir.Mobile.Services.Interfaces;
using Core.Wrappers;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class ProductService : BaseService, IProductService
    {
        private const string getSystemProductsPath = "ProductModule/Product/GetSystemProducts";
        private const string getStoreProductsPath = "ProductModule/Product/GetStoreProducts";
        private const string getStoreProductsByCarrierPath = "ProductModule/Product/GetStoreProductsByCarrier";
        private const string getStoreProductsByStorePath = "ProductModule/Product/GetStoreProductsByStore";
        private const string addProductToStorePath = "ProductModule/Product/AddProductToStore";
        private const string AddOrUpdateProductInCarrierPath = "ProductModule/Product/AddOrUpdateProductInCarrier";
        private const string updateStoreProductPath = "ProductModule/Product/StoreProductUpdate";
        private const string removeProductFromCarrierPath = "ProductModule/Product/StoreProductRemoveFromCarrier";
        private const string removeProductFromStorePath = "ProductModule/Product/StoreProductRemoveFromStore";

        public async Task<ResponseWrapperListing<SystemProductResponse>> GetSystemProducts()
        {
            var restClientRequest = await GetRestClient(Method.POST, getSystemProductsPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<SystemProductResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreProductViewModel>> GetStoreProducts()
        {
            var restClientRequest = await GetRestClient(Method.POST, getStoreProductsPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreProductViewModel>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByCarrier(StoreProdByCarrierRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, getStoreProductsByCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreProdByCarrierResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapperListing<StoreProdByCarrierResponse>> getStoreProductsByStore()
        {
            var restClientRequest = await GetRestClient(Method.POST, getStoreProductsByStorePath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<StoreProdByCarrierResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> AddProductToStore(AddProductToStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addProductToStorePath);
            restClientRequest.Item2.AddParameter("accept","application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> StoreProductUpdate(UpdateProductStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, updateStoreProductPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> AddOrUpdateProductInCarrier(UpdateProductStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, AddOrUpdateProductInCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> StoreProductRemoveFromCarrier(UpdateProductStoreRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, removeProductFromCarrierPath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> StoreProductRemoveFromStore(int storeProductId)
        {
            var restClientRequest = await GetRestClient(Method.DELETE, removeProductFromStorePath);
            restClientRequest.Item2.AddParameter("accept", "application/json");
            restClientRequest.Item2.AddJsonBody(storeProductId);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }


    }
}
