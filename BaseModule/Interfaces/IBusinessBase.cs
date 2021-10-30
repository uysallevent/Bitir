using Core.Wrappers;
using Core.Entities;
using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseModule.Interfaces
{
    public interface IBusinessBase<T>
    where T : class, IEntity, new()
    {
        Task<ResponseWrapper<T>> InsertAsync(T entity);
        Task<ResponseWrapper<IEnumerable<T>>> BulkInsertAsync(IEnumerable<T> entities);
        Task<ResponseWrapper<T>> GetAsync(T entity);
        Task<ResponseWrapper<T>> FindAsync(int Id);
        Task<ResponseWrapperListing<T>> GetAllAsync(T entity = null);
        Task<ResponseWrapper<T>> UpdateAsync(T entity);
        Task<ResponseWrapper<int>> DeleteAsync(T entity);
        Task<ResponseWrapper<int>> DeleteAsync(int Id);
    }
}