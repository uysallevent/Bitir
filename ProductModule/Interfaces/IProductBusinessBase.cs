using BaseModule.Interfaces;
using Bitir.Data.Model.Dtos;
using Core.Entities;
using ProductModule.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductModule.Interfaces
{
    public interface IProductBusinessBase<T>: IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapperListing<Product>> GetSystemProducts();
    }
}
