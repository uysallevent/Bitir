using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using ProductModule.Entities;
using ProductModule.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductModule.Business
{
    public class ProductBusinessBase : BusinessBase<Product>, IProductBusinessBase<Product>
    {
        private IRepository<Product> _productRepository;
        private IRepository<Product_ProductQuantity> _product_ProductQuantityRepository;
        private IUnitOfWork _uow;
        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_ProductQuantity> product_ProductQuantityRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _product_ProductQuantityRepository = product_ProductQuantityRepository;
            _uow = uow;
        }

        public async Task<ResponseWrapperListing<Product>> GetSystemProducts()
        {
            var products = _product_ProductQuantityRepository.GetAll().Select(x =>
            new Product
            {
                Id = x.Id,
                Name = $"{x.Product.Name} - {x.ProductQuantity.Quantity} {x.ProductQuantity.Unit.Name}"
            });
            return new ResponseWrapperListing<Product>(products);
        }
    }
}
