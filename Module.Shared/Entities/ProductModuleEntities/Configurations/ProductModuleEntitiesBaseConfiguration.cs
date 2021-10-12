using Microsoft.EntityFrameworkCore;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductModuleEntitiesBaseConfiguration
    {
        public virtual void Configure(ModelBuilder modelBuilder)
        {
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new ProductStoreConfiguration().Configure(modelBuilder.Entity<Product_Store>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProductPriceConfiguration().Configure(modelBuilder.Entity<ProductStorePrice>());
            new ProductStockConfiguration().Configure(modelBuilder.Entity<ProductStock>());
            new UnitConfiguration().Configure(modelBuilder.Entity<Unit>());
            new ProductQuantityConfiguration().Configure(modelBuilder.Entity<ProductQuantity>());
        }
    }
}
