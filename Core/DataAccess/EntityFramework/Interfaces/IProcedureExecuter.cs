using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Interfaces
{
    public interface IProcedureExecuter<T> where T : class, IProcedureEntity
    {
        Task<List<T>> ExecuteProc(string procDeclaration, params object[] param);
    }
}
