using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Models.Product;
using Bitir.Mobile.Services.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services
{
    public class ProductService : BaseService, IProductService
    {
        private const string getSystemProductsPath = "ProductModule/Product/GetSystemProducts";
        private const string addProductToStorePath = "ProductModule/Product/AddProductToStore";

        public async Task<ResponseWrapperListing<ProductResponse>> GetSystemProducts()
        {
            var restClientRequest = await GetRestClient(Method.POST, getSystemProductsPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<ProductResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }

        public async Task<ResponseWrapper<bool>> AddProductToStore(AddProductToVendorRequest request)
        {
            var restClientRequest = await GetRestClient(Method.POST, addProductToStorePath);
            restClientRequest.Item2.AddParameter("accept","application/json");
            restClientRequest.Item2.AddJsonBody(request);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapper<bool>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }


    }
}
