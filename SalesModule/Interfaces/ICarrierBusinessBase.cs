using BaseModule.Interfaces;

using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace AuthModule.Interfaces
{
    public interface ICarrierBusinessBase<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request);
        Task<ResponseWrapperListing<StoreCarrier>> GetStoreCarriers();
        Task<ResponseWrapperListing<StoreOrderViewModel>> StoreOrders();
        Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request);
    }
}