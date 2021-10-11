using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Enums;
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
        private IRepository<ProductStorePrice> _productStorePriceRepository;
        private IRepository<ProductStock> _productStockRepository;
        private IRepository<Unit> _unitRepository;
        private IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_Store> productStoreRepository,
            IRepository<ProductQuantity> productQuantityRepository,
            IRepository<ProductStorePrice> productStorePriceRepository,
             IRepository<Unit> unitRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<ProductStock> productStockRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _productQuantityRepository = productQuantityRepository;
            _productStockRepository = productStockRepository;
            _productStorePriceRepository = productStorePriceRepository;
            _unitRepository = unitRepository;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ResponseWrapperListing<SystemProductResponse>> GetSystemProducts()
        {
            var products = _productQuantityRepository.GetAll().Select(x =>
                new SystemProductResponse
                {
                    Id = x.Id,
                    Name = $"{x.Product.Name} - {String.Format("{0:0.##}", x.Quantity)} {x.Unit.Name}"
                });
            return Task.FromResult(new ResponseWrapperListing<SystemProductResponse>(products));
        }

        public async Task<ResponseWrapper<bool>> AddProductToStore(AddProductToStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var productStoreCheck = await _productStoreRepository.GetAsync(x => x.ProductQuantityId == request.ProductQuantityId && x.StoreId == storeId);
            if(productStoreCheck!=null)
            {
                throw new BadRequestException("Bu ürün listenizde kayıtlı");
            }

            Product_Store productStore = new Product_Store
            {
                StoreId = storeId,
                ProductQuantityId = request.ProductQuantityId,
                ProductStorePrices = new List<ProductStorePrice>
                    {
                        new ProductStorePrice
                        {
                            Price=request.Price,
                            Status=Status.Active,
                            InsertDate=DateTime.Now,
                            UpdateDate=DateTime.Now
                        }
                    }
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

            var productList = _productStoreRepository
                .GetAll()
                .Join(_productRepository.GetAll().Where(x => x.Status == Status.Active), x => x.ProductQuantityId, y => y.Id, (x, y) => new { Name = y.Name, ProductId = y.Id, StoreId = x.StoreId, ProductStoreId = x.Id })
                .Join(_productQuantityRepository.GetAll(), x => x.ProductId, y => y.ProductId, (x, y) => new { Name = x.Name, Quantity = y.Quantity, ProductId = x.ProductId, StoreId = x.StoreId, ProductStoreId = x.ProductStoreId, UnitId = y.UnitId })
                .Join(_unitRepository.GetAll(), x => x.UnitId, y => y.Id, (x, y) => new { Name = x.Name, Quantity = x.Quantity, UnitName = y.Name, UnitAbbrevation = y.Abbreviation, ProductId = x.ProductId, StoreId = x.StoreId, ProductStoreId = x.ProductStoreId })
                .Join(_productStorePriceRepository.GetAll().Where(x => x.Status == Status.Active), x => x.ProductStoreId, y => y.ProductStoreId, (x, y) => new { Name = x.Name, Quantity = x.Quantity, UnitName = x.Name, UnitAbbrevation = x.UnitAbbrevation, Price = y.Price, ProductId = x.ProductId, StoreId = x.StoreId, ProductStoreId = x.ProductStoreId })
                .Join(_productStockRepository.GetAll().Where(x => x.Status == Status.Active), x => x.ProductStoreId, y => y.ProductStoreId, (x, y) => new { Name = x.Name, Quantity = x.Quantity, StockQuantity = y.Quantity, UnitName = x.Name, UnitAbbrevation = x.UnitAbbrevation, Price = x.Price, ProductId = x.ProductId, StoreId = x.StoreId, ProductStoreId = x.ProductStoreId })
                .Where(x => x.StoreId == storeId)
                .Select(x => new StoreProductResponse { Name = $"{x.Name} {String.Format("{0:0.##}", x.Quantity)} {x.UnitAbbrevation}", StoreId = x.StoreId, Price = x.Price, Quantity = x.StockQuantity, ProductStoreId = x.ProductStoreId });

            return Task.FromResult(new ResponseWrapperListing<StoreProductResponse>(productList));
        }

    }
}
