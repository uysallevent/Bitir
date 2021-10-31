using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.SalesModuleEntities;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseWrapperListing<StoreOrderViewModel>> GetStoreOrders(PagingRequestEntity<StoreOrderViewModel> request);
    }
}
