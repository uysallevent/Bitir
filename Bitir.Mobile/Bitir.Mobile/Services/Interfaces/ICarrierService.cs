using Core.Wrappers;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface ICarrierService
    {
        Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request);
        Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request);
        Task<ResponseWrapperListing<StoreCarrier>> GetStoreCarriers();
    }
}
