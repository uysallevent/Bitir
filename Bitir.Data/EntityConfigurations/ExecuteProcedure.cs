using Bitir.Data.Contexts;
using Bitir.Data.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitir.Data.EntityConfigurations
{
    public class ExecuteProcedure<T> : IExecuteProcedure<T> where T : class, IEntity
    {
        private readonly BitirMainContext _bitirMainContext;
        public ExecuteProcedure(BitirMainContext bitirMainContext)
        {
            _bitirMainContext = bitirMainContext;
        }

        public async Task<List<T>> ExecuteProc(string procDeclaration, params object[] param)
        {
            return await _bitirMainContext.Set<T>().FromSqlRaw(procDeclaration, param).ToListAsync();
        }
    }
}
