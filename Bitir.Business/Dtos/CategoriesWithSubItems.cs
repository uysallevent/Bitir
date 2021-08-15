using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Business.Dtos
{
    public class CategoriesWithSubItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<CategoriesWithSubItems> SubCategories { get; set; }
    }
}
