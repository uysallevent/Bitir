using Core.Entities;
using System.Collections.Generic;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Carrier : BaseEntity
    {   
        public string Plate { get; set; }
        public int? Capacity { get; set; }
        public IEnumerable<Carrier_Store> Carrier_Stores { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
