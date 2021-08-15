using BaseModule.Dal;
using Bitir.Dal.TestDal.Context;
using Bitir.Dal.TestDal.Interfaces;
using Bitir.Entity.TestDb.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitir.Dal.TestDal
{
    public class CategoryDal : DalBase<Category, TestContext>, ICategoryDal
    {
        public List<Category> CategoriesWithSub()
        {
            using (var Context = new TestContext())
            {
                var result = Context.Category.Include("Categories").Where(x => x.ParentId == null);
                return result.ToList();
            }
        }
    }
}
