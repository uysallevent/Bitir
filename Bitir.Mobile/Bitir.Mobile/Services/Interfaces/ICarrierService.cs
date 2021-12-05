using Core.Wrappers;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface ICarrierService
    {
        Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request);
        Task<ResponseWrapper<bool>> AddDistributionZoneToCarrier(CarrierZoneRequest request);
        Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request);
        Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarriers();
        Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarrierById(int carrierId);
        Task<ResponseWrapper<bool>> RemoveZoneFromCarrierById(int carrierDistributionZoneId);
    }
}
