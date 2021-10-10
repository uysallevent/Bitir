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

        public Task<ResponseWrapperListing<Product>> GetSystemProducts()
        {
            var products = _productQuantityRepository.GetAll().Select(x =>
                new Product
                {
                    Id = x.Product.Id,
                    Name = $"{x.Product.Name} - {String.Format("{0:0.##}", x.Quantity)} {x.Unit.Name}"
                });
            return Task.FromResult(new ResponseWrapperListing<Product>(products));
        }

        public async Task<ResponseWrapper<bool>> AddProductToStore(AddProductToVendorRequest request)
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

            return new ResponseWrapper<bool>(true);
        }

        public Task<ResponseWrapperListing<StoreProductResponse>> GetStoreProducts()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var productList =  _productStoreRepository
                .GetAll()
                .Join(_productRepository.GetAll(),x=>x.ProductId,y=>y.Id,(x,y)=>new {Name=y.Name, Id=y.Id,StoreId=x.StoreId })
                .Where(x => x.StoreId == storeId)
                .Select(x=>new StoreProductResponse {Name=x.Name,StoreId=x.StoreId });

            return Task.FromResult(new ResponseWrapperListing<StoreProductResponse>(productList));
        }

    }
}
