using BaseModule.Interfaces;
using Bitir.Business.Dtos;
using Bitir.Entity.TestDb.Product;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Business.Interfaces
{
    public interface ICategoryBusiness : IBusinessBase<Category>
    {
        IDataResult<List<Category>> GetCategoriesWithSubItems();
    }
}
