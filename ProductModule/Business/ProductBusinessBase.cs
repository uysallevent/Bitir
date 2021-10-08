using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using ProductModule.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProductModule.Business
{
    public class ProductBusinessBase : BusinessBase<Product>, IProductService<Product>
    {
        private IRepository<Product> _productRepository;
        private IRepository<ProductQuantity> _productQuantityRepository;
        private IUnitOfWork _uow;
        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<ProductQuantity> productQuantityRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _productQuantityRepository = productQuantityRepository;
            _uow = uow;
        }

        public async Task<ResponseWrapperListing<Product>> GetSystemProducts()
        {

            var products = _productQuantityRepository.GetAll().Select(x =>
                new Product
                {
                    Id = x.Id,
                    Name = $"{x.Product.Name} - {x.Quantity} {x.Unit.Name}"
                });
            return new ResponseWrapperListing<Product>(products);
        }

        public async Task<bool> AddProductToVendor(AddProductToVendorRequest addProductToVendorRequest)
        {

            return true;
        }
    }
}
