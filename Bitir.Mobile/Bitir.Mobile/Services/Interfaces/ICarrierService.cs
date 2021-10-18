using Bitir.Data.Model.Dtos;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface ICarrierService
    {
        Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request);
        Task<ResponseWrapperListing<StoreCarrier>> GetStoreCarriers();
    }
}
