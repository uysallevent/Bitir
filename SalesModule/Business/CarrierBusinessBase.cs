using AuthModule.Interfaces;
using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class CarrierBusinessBase : BusinessBase<Carrier>, ICarrierBusinessBase<Carrier>
    {
        private IRepository<Carrier> _carrierRepository;
        private IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrierBusinessBase(
            IUnitOfWork uow,
            IConfiguration configuration,
            ILoggerFactory logger,
            IRepository<Carrier> carrierRepository,
            IHttpContextAccessor httpContextAccessor) : base(carrierRepository, uow)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger.CreateLogger<CarrierBusinessBase>();
            _carrierRepository = carrierRepository;
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
    }
}