using BaseModule.Interfaces;
using Core.Entities;

namespace AuthModule.Interfaces
{
    public interface ICarrierBusinessBase<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
    }
}