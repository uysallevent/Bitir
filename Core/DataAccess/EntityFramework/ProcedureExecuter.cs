using Core.DataAccess.EntityFramework.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class ProcedureExecuter<TEntity, TContext> : IProcedureExecuter<TEntity>
        where TEntity : class, IProcedureEntity
        where TContext : DbContext
    {
        private readonly DbContext _dbContext;
        public ProcedureExecuter(TContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
        }

        public async Task<List<TEntity>> ExecuteProc(string procDeclaration, params object[] param)
        {
            return await _dbContext.Set<TEntity>().FromSqlRaw(procDeclaration, param).ToListAsync();
        }
    }
}
