using Core.Wrappers;
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
        Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByCarrier(StoreProdByCarrierRequest request);
        Task<ResponseWrapperListing<StoreProdByCarrierResponse>> getStoreProductsByStore();
        Task<ResponseWrapper<bool>> StoreProductUpdate(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> AddOrUpdateProductInCarrier(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductRemoveFromCarrier(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductRemoveFromStore(int storeProductId);
    }
}
