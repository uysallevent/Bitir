using BaseModule.Interfaces;
using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using System.Threading.Tasks;

namespace ProductModule.Interfaces
{
    public interface IProductService<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapperListing<SystemProductResponse>> GetSystemProducts();
        Task<ResponseWrapper<bool>> AddProductToStore(AddProductToStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductAddOrUpdateInCarrier(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductUpdate(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductRemoveFromCarrier(UpdateProductStoreRequest request);
        Task<ResponseWrapper<bool>> StoreProductRemoveFromStore(int storeProductId);
        Task<ResponseWrapperListing<StoreProductViewModel>> GetStoreProducts();
        Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByCarrier(StoreProdByCarrierRequest request);
        Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByStore();
    }
}
