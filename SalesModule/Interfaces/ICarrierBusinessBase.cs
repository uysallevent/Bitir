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
        Task<ResponseWrapper<bool>> AddCarrierToStoreAsync(AddCarrierToStoreRequest request);
        Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarriersAsync();
        Task<ResponseWrapper<bool>> UpdateStoreCarrierAsync(UpdateCarrierToStoreRequest request);
    }
}