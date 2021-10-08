using Core.Entities;
using System.Collections.Generic;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int SubCategoryId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
