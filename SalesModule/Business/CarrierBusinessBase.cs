using AuthModule.Interfaces;
using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class CarrierBusinessBase : BusinessBase<Carrier>, ICarrierBusinessBase<Carrier>
    {
        private readonly IRepository<Carrier> _carrierRepository;
        private readonly IRepository<Carrier_Store> _carrierStoreRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<UserAccount> _userAccountRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<Neighbourhood> _neighbourhoodRepository;
        private readonly IRepository<CarrierDistributionZone> _carrierDistributionZoneRepository;
        private readonly IRepository<StoreCarriersView> _storeCarriersViewRepository;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrierBusinessBase(
            IUnitOfWork uow,
            IConfiguration configuration,
            ILoggerFactory logger,
            IRepository<Carrier> carrierRepository,
            IRepository<Carrier_Store> carrierStoreRepository,
            IRepository<Order> orderRepository,
            IRepository<UserAccount> userAccountRepository,
            IRepository<UserAddress> userAddressRepository,
            IRepository<District> districtRepository,
            IRepository<Province> provinceRepository,
            IRepository<Neighbourhood> neighbourhoodRepository,
        IRepository<CarrierDistributionZone> carrierDistributionZoneRepository,
            IRepository<StoreCarriersView> storeCarriersViewRepository,
            IHttpContextAccessor httpContextAccessor) : base(carrierRepository, uow)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger.CreateLogger<CarrierBusinessBase>();
            _carrierRepository = carrierRepository;
            _carrierStoreRepository = carrierStoreRepository;
            _orderRepository = orderRepository;
            _userAccountRepository = userAccountRepository;
            _userAddressRepository = userAddressRepository;
            _httpContextAccessor = httpContextAccessor;
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _neighbourhoodRepository = neighbourhoodRepository;
            _carrierDistributionZoneRepository = carrierDistributionZoneRepository;
            _storeCarriersViewRepository = storeCarriersViewRepository;
        }

        public async Task<ResponseWrapper<bool>> AddCarrierToStoreAsync(AddCarrierToStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            await _carrierRepository.AddAsync(new Carrier
            {
                Capacity = request.Capacity,
                Plate = request.Plate,
                DriverName = request.DriverName,
                Carrier_Stores = new List<Carrier_Store>
                {
                    new Carrier_Store
                    {
                        StoreId=storeId,
                        Status=Core.Enums.Status.Active,
                        InsertDate=DateTime.Now,
                        UpdateDate=DateTime.Now
                    }
                }
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Araç bilgileri eklenemedi");
            }


            return new ResponseWrapper<bool>(true);
        }

        public async Task<ResponseWrapper<bool>> UpdateStoreCarrierAsync(UpdateCarrierToStoreRequest request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            _carrierStoreRepository.Update(new Carrier_Store
            {
                Id = request.CarrierStoreId,
                CarrierId = request.CarrierId,
                StoreId = storeId,
                Status = request.Status
            });

            _carrierRepository.Update(new Carrier
            {
                Id = request.CarrierId,
                Plate = request.Plate,
                Capacity = request.Capacity,
                DriverName = request.DriverName,
                Status = request.Status
            });

            if (request.LocalityNames != null && request.LocalityNames.Any())
            {
                var neigborhoodIds = _neighbourhoodRepository.GetAll(x => x.DistrictId == request.DistrictId && request.LocalityNames.Contains(x.LocalityName));
                await _carrierDistributionZoneRepository.AddRangeAsync(neigborhoodIds.Select(x => new CarrierDistributionZone
                {
                    CarrierId = request.CarrierId,
                    ProvinceId = request.ProvinceId,
                    DistrictId = request.DistrictId,
                    NeighbourhoodId = x.Id,
                    UpdateDate = DateTime.UtcNow,
                }));
            }
            else
            {
                if (request.CarrierDistributionZoneId == null)
                {
                    await _carrierDistributionZoneRepository.AddAsync(new CarrierDistributionZone
                    {
                        CarrierId = request.CarrierId,
                        ProvinceId = request.ProvinceId,
                        DistrictId = request.DistrictId == 0 ? null : request.DistrictId,
                    });
                }
                else
                {
                    _carrierDistributionZoneRepository.Update(new CarrierDistributionZone
                    {
                        Id = request.CarrierDistributionZoneId ?? 0,
                        CarrierId = request.CarrierId,
                        ProvinceId = request.ProvinceId,
                        DistrictId = request.DistrictId == 0 ? null : request.DistrictId,
                    });
                }
            }


            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Araç bilgileri güncellenemedi");
            }

            return new ResponseWrapper<bool>(true);
        }

        public async Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarriersAsync()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _storeCarriersViewRepository.GetAll(x => x.StoreId == storeId && x.CarrierStatus == Core.Enums.Status.Active).ToListAsync();

            return new ResponseWrapperListing<StoreCarriersView>(result);
        }

        public async Task<ResponseWrapperListing<StoreCarriersView>> GetStoreCarriersAsync(int carrierId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _storeCarriersViewRepository.GetAll(x =>
            x.CarrierId == carrierId
            && x.StoreId == storeId
            && x.CarrierStatus == Core.Enums.Status.Active).AsNoTracking().ToListAsync();
            return new ResponseWrapperListing<StoreCarriersView>(result);
        }

        public async Task<ResponseWrapper<bool>> AddDistributionZoneToCarrier(CarrierZoneRequest request)
        {
            var check = await _carrierDistributionZoneRepository.GetAll(x => (
                (x.CarrierId == request.CarrierId && x.ProvinceId == request.ProvinceId)
             || (x.CarrierId == request.CarrierId && x.DistrictId == request.DistrictId)
             || (x.CarrierId == request.CarrierId && request.NeighborhoodIds.Contains(x.NeighbourhoodId)))
             && x.Status == Core.Enums.Status.Active).ToListAsync();

            if (check != null)
            {
                throw new BadRequestException("Bu bölge taşıyıcı için önceden eklenmiş");
            }

            if (request.NeighborhoodIds != null && request.NeighborhoodIds.Any())
            {
                await _carrierDistributionZoneRepository.AddRangeAsync(request.NeighborhoodIds.Select(x => new CarrierDistributionZone
                {
                    CarrierId = request.CarrierId,
                    ProvinceId = request.ProvinceId,
                    DistrictId = request.DistrictId,
                    NeighbourhoodId = x
                }));
            }
            else
            {
                await _carrierDistributionZoneRepository.AddAsync(new CarrierDistributionZone
                {
                    CarrierId = request.CarrierId,
                    ProvinceId = request.ProvinceId,
                    DistrictId = request.DistrictId,
                });
            }

            await _uow.SaveChangesAsync();
            return new ResponseWrapper<bool>(true);
        }

        public async Task<ResponseWrapper<bool>> RemoveZoneFromCarrier(int carrierDistributionZoneId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var check = await _carrierDistributionZoneRepository.GetAsync(x => x.Id == carrierDistributionZoneId && x.Status == Core.Enums.Status.Active);
            if (check == null)
            {
                throw new BadRequestException("Taşıyıcı kaydı bulunamadı");
            }

            check.Status = Core.Enums.Status.Deleted;
            _carrierDistributionZoneRepository.Update(check);
            await _uow.SaveChangesAsync();

            return new ResponseWrapper<bool>(true);
        }


    }
}