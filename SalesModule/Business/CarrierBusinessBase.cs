using AuthModule.Interfaces;
using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;

namespace AuthModule.Business
{
    public class CarrierBusinessBase : BusinessBase<Carrier>, ICarrierBusinessBase<Carrier>
    {
        private IRepository<Carrier> _carrierRepository;
        private IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CarrierBusinessBase(
            IUnitOfWork uow,
            IConfiguration configuration,
            ILoggerFactory logger,
            IRepository<Carrier> carrierRepository) : base(carrierRepository, uow)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger.CreateLogger<CarrierBusinessBase>();
            _carrierRepository = carrierRepository;
        }
    }
}