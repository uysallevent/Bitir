using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Models.Product;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseWrapperListing<ProductResponse>> GetSystemProducts();
        Task<ResponseWrapper<bool>> AddProductToStore(AddProductToVendorRequest request);
    }
}
