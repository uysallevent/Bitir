using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductModuleEntitiesBaseConfiguration
    {
        public virtual void Configure(ModelBuilder modelBuilder)
        {
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new ProductStoreConfiguration().Configure(modelBuilder.Entity<ProductStore>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProductPriceConfiguration().Configure(modelBuilder.Entity<ProductPrice>());
            new ProductStockConfiguration().Configure(modelBuilder.Entity<ProductStock>());
            new UnitConfiguration().Configure(modelBuilder.Entity<Unit>());
        }
    }
}
