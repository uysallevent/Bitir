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

        public async Task<ResponseWrapperListing<ProductResponse>> GetSystemProducts()
        {
            var restClientRequest = await GetRestClient(Method.POST, getSystemProductsPath);
            var restResponse = await restClientRequest.Item1.ExecuteAsync<ResponseWrapperListing<ProductResponse>>(restClientRequest.Item2);
            return ResponseHandler(restResponse);
        }


    }
}
