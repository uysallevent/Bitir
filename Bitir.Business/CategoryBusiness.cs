using BaseModule.Business;
using BaseModule.Dal;
using BaseModule.Interfaces;
using Bitir.Business.Interfaces;
using Bitir.Entity.TestDb.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Business
{
    public class CategoryBusiness : BusinessBase<Category>, ICategoryBusinessBase
    {
        private readonly IDalBase<Category> _dalBase;
        public CategoryBusiness(IDalBase<Category> dalBase) : base(dalBase)
        {
            _dalBase = dalBase;

        }
    }
}
