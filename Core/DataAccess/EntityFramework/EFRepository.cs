using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EFRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EFRepository(TContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region IRepository Members
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            InsertInitializer(entity, Status.Active);
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            UpdateInitializer(entity);
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            foreach (var property in _dbContext.Entry(entity).Properties)
            {
                if (property.CurrentValue == null)
                    _dbContext.Entry(entity).Property(property.Metadata.Name).IsModified = false;
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity.GetType().GetProperty("Status") != null)
            {
                TEntity _entity = entity;
                _entity.GetType().GetProperty("Status").SetValue(_entity, 0);
                this.Update(_entity);
            }
            else
            {
                var dbEntityEntry = _dbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    TEntity _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
        #endregion

        private static void InsertInitializer(TEntity entity, Status status)
        {
            var insertDateProp = entity.GetType().GetProperty("InsertDate");
            if (insertDateProp != null)
            {
                Type t = Nullable.GetUnderlyingType(insertDateProp.PropertyType) ?? insertDateProp.PropertyType;
                object safeValue = Convert.ChangeType(DateTime.Now, t);
                insertDateProp.SetValue(entity, safeValue, null);
            }

            var updateDateProp = entity.GetType().GetProperty("UpdateDate");
            if (entity.GetType().GetProperties().Any(x => x.Name == "UpdateDate"))
            {
                Type t = Nullable.GetUnderlyingType(updateDateProp.PropertyType) ?? updateDateProp.PropertyType;
                object safeValue = Convert.ChangeType(DateTime.Now, t);
                updateDateProp.SetValue(entity, safeValue, null);
            }

            var statusProp = entity.GetType().GetProperty("Status");
            if (entity.GetType().GetProperties().Any(x => x.Name == "Status"))
            {
                Type t = Nullable.GetUnderlyingType(statusProp.PropertyType) ?? statusProp.PropertyType;
                object safeValue = Convert.ChangeType(status, t);
                statusProp.SetValue(entity, safeValue, null);
            }
        }

        private static void UpdateInitializer(TEntity entity)
        {
            var updateDateProp = entity.GetType().GetProperty("UpdateDate");
            if (entity.GetType().GetProperties().Any(x => x.Name == "UpdateDate"))
            {
                Type t = Nullable.GetUnderlyingType(updateDateProp.PropertyType) ?? updateDateProp.PropertyType;
                object safeValue = Convert.ChangeType(DateTime.Now, t);
                updateDateProp.SetValue(entity, safeValue, null);
            }
        }


        void test2(TEntity entity, Status status)
        {
            foreach (var item in entity.GetType().GetProperties())
            {
                var propertyInstance = item.GetValue(entity, null);
                var mainObjectsProperties = item.PropertyType.GetProperties();
                foreach (var property in mainObjectsProperties)
                {
                    // get the actual instance of this 2nd level property
                    var leafInstance = property.GetValue(propertyInstance, null);
                    InsertInitializer(entity, status);

                }
                if (mainObjectsProperties.Any())
                    continue;

                InsertInitializer(entity, status);
            }
        }
    }
}
