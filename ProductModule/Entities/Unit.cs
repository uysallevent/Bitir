using Core.Entities;
using System.Collections.Generic;

namespace ProductModule.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public IEnumerable<Unit> Product { get; set; }

    }
}
