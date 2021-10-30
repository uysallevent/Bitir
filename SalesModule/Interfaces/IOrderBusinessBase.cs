using BaseModule.Interfaces;
using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.SalesModuleEntities;
using System.Threading.Tasks;

namespace AuthModule.Interfaces
{
    public interface IOrderBusinessBase<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapperListing<StoreOrderViewModel>> StoreOrders();
    }
}