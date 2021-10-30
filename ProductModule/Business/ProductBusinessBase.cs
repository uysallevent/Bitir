using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Enums;
using Core.Exceptions;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private IRepository<Product_Store> _productStoreRepository;
        private IRepository<ProductQuantity> _productQuantityRepository;
        private IRepository<ProductStorePrice> _productStorePriceRepository;
        private IRepository<ProductStock> _productStockRepository;
        private IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProcedureExecuter<StoreProductViewModel> _executeStoreProductProcedure;
        private readonly IProcedureExecuter<StoreProductByCarrierViewModel> _executeStoreProdByCarrierProcedure;
        private readonly IProcedureExecuter<StoreProductByStoreViewModel> _executeStoreProdByStoreProcedure;
        public ProductBusinessBase(
            IUnitOfWork uow,
            IRepository<Product> productRepository,
            IRepository<Product_Store> productStoreRepository,
            IRepository<ProductQuantity> productQuantityRepository,
            IRepository<ProductStorePrice> productStorePriceRepository,
            IHttpContextAccessor httpContextAccessor,
            IProcedureExecuter<StoreProductViewModel> executeStoreProductProcedure,
            IProcedureExecuter<StoreProductByCarrierViewModel> executeStoreProdByCarrierProcedure,
            IProcedureExecuter<StoreProductByStoreViewModel> executeStoreProdByStoreProcedure,
            IRepository<ProductStock> productStockRepository) : base(productRepository, uow)
        {
            _productStoreRepository = productStoreRepository;
            _productQuantityRepository = productQuantityRepository;
            _productStockRepository = productStockRepository;
            _productStorePriceRepository = productStorePriceRepository;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _executeStoreProductProcedure = executeStoreProductProcedure;
            _executeStoreProdByCarrierProcedure = executeStoreProdByCarrierProcedure;
            _executeStoreProdByStoreProcedure = executeStoreProdByStoreProcedure;
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

            var productStoreCheck = await _productStoreRepository.GetAsync(x => x.ProductQuantityId == request.ProductQuantityId && x.StoreId == storeId && x.Status == Status.Active);
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

            var productStockResult = await _productStockRepository.GetAsync(x => (x.Status == Status.Active || x.Status == Status.Pasive) && x.Id == request.ProductStockId);
            if (productStockResult == null)
            {
                throw new BadRequestException("Ürün stok bilgisi bulunamadı");
            }

            _productStockRepository.Update(new ProductStock
            {
                Id = request.ProductStockId ?? productStockResult.Id,
                ProductStoreId = request.ProductStoreId,
                Quantity = request.Quantity,
                CarrierId = request.CarrierId,
                Status = request.Status
            });

            if (request.ProductPriceId > 0)
            {
                _productStorePriceRepository.Update(new ProductStorePrice
                {
                    Id = request.ProductPriceId,
                    Price = request.Price,
                    ProductStoreId = request.ProductStoreId,
                    Status = request.Status
                });
            }


            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Ürün bilgileri güncellenemedi");
            }

            return new ResponseWrapper<bool>(true);

        }

        public async Task<ResponseWrapper<bool>> StoreProductAddOrUpdateInCarrier(UpdateProductStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var productStockResult = _productStockRepository.GetAll(x => x.Status == Status.Active && x.ProductStoreId == request.ProductStoreId).AsNoTracking();

            var inStore = productStockResult.FirstOrDefault(x => x.CarrierId == null);
            var inVehicle = productStockResult.FirstOrDefault(x => x.CarrierId == request.CarrierId);
            if (inStore.Quantity - request.Quantity < 0)
            {
                throw new BadRequestException("Depo ürün stoğunuz yeterli değil");
            }

            if (inVehicle == null)
            {
                await _productStockRepository.AddAsync(new ProductStock
                {
                    ProductStoreId = request.ProductStoreId,
                    Quantity = request.Quantity,
                    CarrierId = request.CarrierId
                });
            }
            else
            {
                _productStockRepository.Update(new ProductStock
                {
                    Id = inVehicle.Id,
                    ProductStoreId = inVehicle.ProductStoreId,
                    Quantity = inVehicle.Quantity + request.Quantity
                });
            }

            _productStockRepository.Update(new ProductStock
            {
                Id = inStore.Id,
                ProductStoreId = inStore.ProductStoreId,
                Quantity = inStore.Quantity - request.Quantity
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Araçta ürün güncellemesi yapılamadı");
            }

            return new ResponseWrapper<bool>(true);

        }

        public async Task<ResponseWrapper<bool>> StoreProductRemoveFromCarrier(UpdateProductStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var productStockResult = _productStockRepository.GetAll(x => x.Status == Status.Active && x.ProductStoreId == request.ProductStoreId).AsNoTracking();
            if (productStockResult == null || !productStockResult.Any())
            {
                throw new BadRequestException("Ürün stok bilgisi bulunamadı");
            }

            var removeItem = productStockResult.FirstOrDefault(x => x.Id == request.ProductStockId);
            if (removeItem == null)
            {
                throw new BadRequestException("Ürün stok bilgisi bulunamadı");
            }

            _productStockRepository.Update(new ProductStock
            {
                Id = removeItem.Id,
                ProductStoreId = removeItem.ProductStoreId,
                Quantity = 0,
                CarrierId = removeItem.CarrierId,
                Status = request.Status
            });

            var storeItem = productStockResult.FirstOrDefault(x => x.CarrierId == null);

            _productStockRepository.Update(new ProductStock
            {
                Id = storeItem.Id,
                ProductStoreId = storeItem.ProductStoreId,
                Quantity = storeItem.Quantity + removeItem.Quantity,
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Ürün bilgileri güncellenemedi");
            }

            return new ResponseWrapper<bool>(true);

        }

        public async Task<ResponseWrapper<bool>> StoreProductRemoveFromStore(int storeProductId)
        {
            var storeProduct = await _productStoreRepository.GetAsync(x => x.Id == storeProductId);
            if (storeProduct == null)
            {
                throw new BadRequestException("Ürün bulunamadı");
            }
            var storeProductStocks = _productStockRepository.GetAll(x => x.ProductStoreId == storeProductId).AsNoTracking();
            if (storeProductStocks != null && storeProductStocks.Any())
            {
                await storeProductStocks.ForEachAsync(x =>
                {
                    x.Status = Status.Deleted;
                    _productStockRepository.Update(x);
                });
            }

            storeProduct.Status = Status.Deleted;
            _productStoreRepository.Update(storeProduct);

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Ürün başarı ile silindi");
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

            var result = await _executeStoreProdByCarrierProcedure.ExecuteProc("EXEC product.GetStoreProductsByCarrier {0},{1}", new object[] { request.CarrierId, storeId });
            if (result != null && result.Any())
            {
                return new ResponseWrapperListing<StoreProdByCarrierResponse>(result.Select(x => new StoreProdByCarrierResponse
                {
                    ProductName = x.ProductName,
                    Abbreviation = x.Abbreviation,
                    Quantity = x.Quantity,
                    UnitName = x.UnitName,
                    Capacity = x.Capacity,
                    ProductStoreId = x.ProductStoreId,
                    ProductStockId = x.ProductStockId,
                    ProductStock = x.ProductStock
                }));
            }

            return new ResponseWrapperListing<StoreProdByCarrierResponse>(null);
        }

        public async Task<ResponseWrapperListing<StoreProdByCarrierResponse>> GetStoreProductsByStore()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _executeStoreProdByStoreProcedure.ExecuteProc("EXEC product.GetStoreProductsByStore {0}", new object[] { storeId });
            if (result != null && result.Any())
            {
                return new ResponseWrapperListing<StoreProdByCarrierResponse>(result.Select(x => new StoreProdByCarrierResponse
                {
                    ProductName = x.ProductName,
                    Abbreviation = x.Abbreviation,
                    Quantity = x.Quantity,
                    UnitName = x.UnitName,
                    Capacity = x.Capacity ?? 0,
                    ProductStockId = x.ProductStockId,
                    ProductStoreId = x.ProductStoreId,
                    ProductStock = x.ProductStock
                }));
            }

            return new ResponseWrapperListing<StoreProdByCarrierResponse>(null);
        }

    }
}
