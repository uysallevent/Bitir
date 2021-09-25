using Core.DataAccess;
using Core.Entities;
using System;

namespace BaseModule.Interfaces
{
    public interface IDalBase<T> : IEntityRepository<T>
        where T : class, IEntity, new()
    {
        DateTime GetSqlServerUtcNow();
    }
}