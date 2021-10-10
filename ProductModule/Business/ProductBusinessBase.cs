using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
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
        private IRepository<ProductStock> _productStockRepository;
        private IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_Store> productStoreRepository,
            IRepository<ProductQuantity> productQuantityRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<ProductStock> productStockRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _productQuantityRepository = productQuantityRepository;
            _productStockRepository = productStockRepository;
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

        public async Task<ResponseWrapper<Product_Store>> AddProductToVendor(AddProductToVendorRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

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
                },

            };
            await _productStoreRepository.AddAsync(productStore);
            await _productStockRepository.AddAsync(new ProductStock
            {
                ProductStore = productStore,
                Quantity = request.Quantity
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Ürün bilgileri eklenemedi");
            }

            return new ResponseWrapper<Product_Store>(productStore);
        }

    }
}
