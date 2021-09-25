using BaseModule.Business;
using Bitir.Business.Interfaces;
using Bitir.Dal.TestDal.Interfaces;
using Bitir.Entity.TestDb.Product;
using Core.Utilities.Results;
using System.Collections.Generic;
using System.Linq;

namespace Bitir.Business
{
    public class CategoryBusiness : BusinessBase<Category>, ICategoryBusiness
    {
        private readonly ICategoryDal _dalBase;
        public CategoryBusiness(ICategoryDal dalBase) : base(dalBase)
        {
            _dalBase = dalBase;
        }

        public IDataResult<List<Category>> GetCategoriesWithSubItems()
        {
            var allCategories = _dalBase.CategoriesWithSub().ToList();
            return new SuccessDataResult<List<Category>>(allCategories);
        }


    }
}
