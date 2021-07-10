using BaseModule.Business;
using BaseModule.Dal;
using BaseModule.Interfaces;
using Bitir.Business.Interfaces;
using Bitir.Dal.TestDal.Interfaces;
using Bitir.Entity.TestDb.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Business
{
    public class CategoryBusiness : BusinessBase<Category>, ICategoryBusiness
    {
        private readonly ICategoryDal _dalBase;
        public CategoryBusiness(ICategoryDal dalBase) : base(dalBase)
        {
            _dalBase = dalBase;

        }
    }
}
