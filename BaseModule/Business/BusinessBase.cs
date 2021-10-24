using BaseModule.Interfaces;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Entities;
using Core.Utilities.ExpressionGenerator;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModule.Business
{
    public class BusinessBase<T> : IBusinessBase<T>
    where T : class, IEntity, new()
    {
        private IRepository<T> _repositoryBase;
        private IUnitOfWork _uow;

        public BusinessBase(IRepository<T> repositoryBase, IUnitOfWork uow)
        {
            _repositoryBase = repositoryBase;
            _uow = uow;
        }

        public virtual async Task<ResponseWrapper<T>> InsertAsync(T entity)
        {
            await _repositoryBase.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return new ResponseWrapper<T>(entity);
        }

        public virtual async Task<ResponseWrapper<IEnumerable<T>>> BulkInsertAsync(IEnumerable<T> entities)
        {
            await _repositoryBase.AddRangeAsync(entities);
            await _uow.SaveChangesAsync();
            return new ResponseWrapper<IEnumerable<T>>(entities);
        }

        public virtual async Task<ResponseWrapper<T>> GetAsync(T entity)
        {
            var filter = ExpressionGenerator<T, T>.Generate(entity);
            var result = await _repositoryBase.GetAsync(filter);
            return new ResponseWrapper<T>(result);
        }

        public virtual async Task<ResponseWrapper<T>> FindAsync(int Id)
        {
            var result = await _repositoryBase.GetByIdAsync(Id);
            if (result != null)
            {
                return new ResponseWrapper<T>(result);
            }
            return new ResponseWrapper<T>(null);
        }

        public virtual async Task<ResponseWrapperListing<T>> GetAllAsync(T entity = null)
        {
            var filter = entity != null ? ExpressionGenerator<T, T>.Generate(entity) : null;
            var result =await _repositoryBase.GetAll(filter).ToListAsync();
            if (result != null && result.Any())
            {
                return new ResponseWrapperListing<T>(result);
            }
            return new ResponseWrapperListing<T>(null);
        }

        public virtual async Task<ResponseWrapper<T>> UpdateAsync(T entity)
        {
            _repositoryBase.Update(entity);
            await _uow.SaveChangesAsync();
            return new ResponseWrapper<T>(entity);
        }

        public virtual async Task<ResponseWrapper<int>> DeleteAsync(T entity)
        {
            _repositoryBase.Delete(entity);
            var result = await _uow.SaveChangesAsync();
            return new ResponseWrapper<int>(result);
        }

        public virtual async Task<ResponseWrapper<int>> DeleteAsync(int Id)
        {
            _repositoryBase.Delete(Id);
            var result = await _uow.SaveChangesAsync();
            return new ResponseWrapper<int>(result);
        }
    }
}