using Core.Entities;
using System.Collections.Generic;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; set; }

    }
}
