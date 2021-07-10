using BaseModule.Dal;
using Bitir.Dal.TestDal.Context;
using Bitir.Dal.TestDal.Interfaces;
using Bitir.Entity.TestDb.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Dal.TestDal
{
    public class CategoryDal: DalBase<Category,TestContext>, ICategoryDal
    {
    }
}
