using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Http;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using ProductModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductModule.Business
{
    public class ProductBusinessBase : BusinessBase<Product>, IProductService<Product>
    {
        private IRepository<Product> _productRepository;
        private IRepository<Product_Store> _productStoreRepository;
        private IRepository<ProductQuantity> _productQuantityRepository;
        private IRepository<ProductStorePrice> _productStorePriceRepository;
        private IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_Store> productStoreRepository,
            IRepository<ProductQuantity> productQuantityRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<ProductStorePrice> productStorePriceRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _productQuantityRepository = productQuantityRepository;
            _productStorePriceRepository = productStorePriceRepository;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseWrapperListing<Product>> GetSystemProducts()
        {

            var products = _productQuantityRepository.GetAll().Select(x =>
                new Product
                {
                    Id = x.Id,
                    Name = $"{x.Product.Name} - {String.Format("{0:0.##}", x.Quantity)} {x.Unit.Name}"
                });
            return new ResponseWrapperListing<Product>(products);
        }

        public async Task<bool> AddProductToVendor(AddProductToVendorRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);

            var productStore = new Product_Store
            {
                StoreId = storeId,
                ProductId = request.ProductId,
                ProductStorePrices = new List<ProductStorePrice>
                {
                    new ProductStorePrice
                    {
                        Price=request.Price,
                        Status=Core.Enums.Status.Active,
                        InsertDate=DateTime.Now,
                        UpdateDate=DateTime.Now
                    }
                }
            };
            await _productStoreRepository.AddAsync(productStore);

            var result = await _uow.SaveChangesAsync();

            return true;
        }
    }
}
