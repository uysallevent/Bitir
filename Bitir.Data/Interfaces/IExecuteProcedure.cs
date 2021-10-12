using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitir.Data.Interfaces
{
    public interface IExecuteProcedure<T> where T : class, IEntity
    {
        Task<List<T>> ExecuteProc(string procDeclaration, params object[] param);
    }
}
