
using Core.Entities;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.ProductModuleEntities;
using System.Collections.Generic;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Store:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<ProductStore> ProductStores { get; set; }
        public IEnumerable<Store_UserAccount> Store_UserAccounts { get; set; }
        public IEnumerable<Carrier_Store> Carrier_Stores { get; set; }

    }
}
