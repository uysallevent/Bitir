using Microsoft.EntityFrameworkCore;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.AuthModuleEntities.Configuration;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.ProductModuleEntities.Configuration;
using Module.Shared.Entities.SalesModuleEntities;
using Module.Shared.Entities.SalesModuleEntities.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Bitir.Data.Contexts
{
    public class BitirMainContext : DbContext
    {
        public BitirMainContext(DbContextOptions<BitirMainContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Neighbourhood> Neighbourhood { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Product_Store> ProductStore { get; set; }
        public DbSet<ProductStorePrice> ProductPrice { get; set; }
        public DbSet<ProductStock> ProductStock { get; set; }
        public DbSet<Carrier_Store> Carrier_Store { get; set; }
        public DbSet<Carrier> Carrier { get; set; }
        public DbSet<CarrierDistributionZone> CarrierDistributionZone { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Store_UserAccount> Store_UserAccount { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        //[NotMapped]
        //public DbSet<StoreOrdersView> StoreOrdersView { get; set; }
        //[NotMapped]
        //public DbSet<StoreProductViewModel> StoreProductViewModel { get; set; }
        //[NotMapped]
        //public DbSet<StoreProductByCarrierViewModel> StoreProductByCarrierViewModel { get; set; }
        //[NotMapped]
        //public DbSet<StoreProductByStoreViewModel> StoreProductByStoreViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AuthModuleEntitiesBaseConfiguration().Configure(modelBuilder);
            new ProductModuleEntitiesBaseConfiguration().Configure(modelBuilder);
            new SalesModuleEntitiesBaseConfiguration().Configure(modelBuilder);
        }


    }
}
