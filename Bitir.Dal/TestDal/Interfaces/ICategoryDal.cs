using BaseModule.Dal;
using Bitir.Entity.TestDb.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Dal.TestDal.Interfaces
{
    public interface ICategoryDal: IDalBase<Category>
    {
        List<Category> CategoriesWithSub();
    }
}
