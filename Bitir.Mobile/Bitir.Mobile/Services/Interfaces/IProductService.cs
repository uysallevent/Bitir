using Bitir.Data.Model.Dtos;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseWrapperListing<SystemProductResponse>> GetSystemProducts();
        Task<ResponseWrapper<bool>> AddProductToStore(AddProductToStoreRequest request);
        Task<ResponseWrapperListing<StoreProductViewModel>> GetStoreProducts();
    }
}
