using BaseModule.Business;
using Bitir.Data.Interfaces;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
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
        private IRepository<Carrier> _carrierRepository;
        private IRepository<Unit> _unitRepository;
        private IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExecuteProcedure<StoreProductViewModel> _executeStoreProductProcedure;
        private readonly IExecuteProcedure<StoreProductByCarrierViewModel> _executeStoreProdByCarrierProcedure;
        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_Store> productStoreRepository,
            IRepository<ProductQuantity> productQuantityRepository,
            IRepository<ProductStorePrice> productStorePriceRepository,
            IRepository<Carrier> carrierRepository,
            IRepository<Unit> unitRepository,
            IHttpContextAccessor httpContextAccessor,
            IExecuteProcedure<StoreProductViewModel> executeStoreProductProcedure,
            IExecuteProcedure<StoreProductByCarrierViewModel> executeStoreProdByCarrierProcedure,
            IRepository<ProductStock> productStockRepository) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _productQuantityRepository = productQuantityRepository;
            _productStockRepository = productStockRepository;
            _productStorePriceRepository = productStorePriceRepository;
            _carrierRepository = carrierRepository;
            _unitRepository = unitRepository;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _executeStoreProductProcedure = executeStoreProductProcedure;
            _executeStoreProdByCarrierProcedure = executeStoreProdByCarrierProcedure;
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
            if (productStoreCheck != null)
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

        public async Task<ResponseWrapper<bool>> StoreProductUpdate(UpdateProductStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var productStoreCheck = await _productStoreRepository.GetAsync(x => x.ProductQuantityId == request.ProductQuantityId && x.StoreId == storeId);
            if (productStoreCheck == null)
            {
                throw new BadRequestException("Bu ürün listenizde bulunamadı");
            }

            var productStockResult = _productStockRepository.GetAll(x => x.Status == Status.Active && x.ProductStoreId == request.ProductStoreId).AsNoTracking();
            if (request.CarrierId == null && productStockResult.Any(x => x.CarrierId == null))
            {
                var productInStore = productStockResult.FirstOrDefault(x => x.CarrierId == null);
                _productStockRepository.Update(new ProductStock
                {
                    Id = productInStore.Id,
                    ProductStoreId = request.ProductStoreId,
                    Quantity = (request.Quantity != 0) ? (request.Quantity + productInStore.Quantity) : 0
                });
            }
            else if (request.CarrierId == null && !productStockResult.Any(x => x.CarrierId == null))
            {
                await _productStockRepository.AddAsync(new ProductStock
                {
                    ProductStoreId = request.ProductStoreId,
                    Quantity = request.Quantity
                });
            }
            else if (request.CarrierId != null && productStockResult.Any(x => x.CarrierId == request.CarrierId))
            {
                var productInVehicle = productStockResult.FirstOrDefault(x => x.CarrierId == request.CarrierId);
                _productStockRepository.Update(new ProductStock
                {
                    Id = productInVehicle.Id,
                    ProductStoreId = request.ProductStoreId,
                    Quantity = (request.Quantity + productInVehicle.Quantity),
                    CarrierId = request.CarrierId
                });
            }
            else if (request.CarrierId != null && !productStockResult.Any(x => x.CarrierId == request.CarrierId))
            {
                await _productStockRepository.AddAsync(new ProductStock
                {
                    ProductStoreId = request.ProductStoreId,
                    Quantity = request.Quantity,
                    CarrierId = request.CarrierId
                });
            }

            _productStoreRepository.Update(new Product_Store
            {
                Id = request.ProductStoreId,
                ProductQuantityId = request.ProductQuantityId,
                StoreId = storeId,
                Status = request.Status
            });
            _productStorePriceRepository.Update(new ProductStorePrice
            {
                Id = request.ProductPriceId,
                Price = request.Price,
                ProductStoreId = request.ProductStoreId
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Ürün bilgileri güncellenemedi");
            }

            return new ResponseWrapper<bool>(true);

        }

        public async Task<ResponseWrapperListing<StoreProductViewModel>> GetStoreProducts()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _executeStoreProductProcedure.ExecuteProc("EXEC product.GetStoreProducts {0}", new object[] { storeId });

            return new ResponseWrapperListing<StoreProductViewModel>(result);
        }

        public async Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByCarrier(StoreProdByCarrierRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _executeStoreProdByCarrierProcedure.ExecuteProc("EXEC product.GetStoreProductsByCarrier {0}", new object[] { request.CarrierId });
            if (result != null && result.Any())
            {
                return new ResponseWrapperListing<StoreProdByCarrierResponse>(result.Select(x => new StoreProdByCarrierResponse
                {
                    CarrierId = request.CarrierId,
                    Plate = x.Plate,
                    ProductStock = x.ProductStock
                }));
            }

            return new ResponseWrapperListing<StoreProdByCarrierResponse>(null);
        }

    }
}
