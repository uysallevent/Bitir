﻿using AuthModule.Interfaces;
using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private IRepository<Carrier> _carrierRepository;
        private IRepository<Carrier_Store> _carrierStoreRepository;
        private IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrierBusinessBase(
            IUnitOfWork uow,
            IConfiguration configuration,
            ILoggerFactory logger,
            IRepository<Carrier> carrierRepository,
            IRepository<Carrier_Store> carrierStoreRepository,
            IHttpContextAccessor httpContextAccessor) : base(carrierRepository, uow)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger.CreateLogger<CarrierBusinessBase>();
            _carrierRepository = carrierRepository;
            _carrierStoreRepository = carrierStoreRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseWrapper<bool>> AddCarrierToStore(AddCarrierToStoreRequest request)
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

        public async Task<ResponseWrapper<bool>> UpdateStoreCarrier(UpdateCarrierToStoreRequest request)
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
            });

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Araç bilgileri güncellenemedi");
            }

            return new ResponseWrapper<bool>(true);
        }

        public async Task<ResponseWrapperListing<StoreCarrier>> GetStoreCarriers()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }

            var result = await _carrierRepository.GetAll().Join(_carrierStoreRepository.GetAll().Where(x => x.StoreId == storeId), x => x.Id, y => y.CarrierId, (x, y) => new { carrierId = x.Id, x.Plate, x.Capacity, carrierStoreId = y.Id, y.Status }).Select(x => new StoreCarrier { CarrierId = x.carrierId, CarrierStoreId = x.carrierStoreId, Plate = x.Plate, Capacity = x.Capacity, Status = x.Status }).ToListAsync();

            if (result == null || !result.Any())
            {
                throw new BadRequestException("Araç bilgileri bulunamadı");
            }

            return new ResponseWrapperListing<StoreCarrier>(result);
        }
    }
}