using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.Dapper.Interfaces
{
    public interface IDapperQueryGenerator<in T> where T : IEntity
    {
        string AddQueryGenerator(T model);


    }
}
