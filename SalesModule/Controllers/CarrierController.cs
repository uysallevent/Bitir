using AuthModule.Interfaces;
using BaseModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;

namespace AuthModule.Controllers
{
    [ApiController]
    public class CarrierController : ActionBaseController<Carrier>
    {
        private readonly ICarrierBusinessBase<Carrier> _carrierBusinessBase;
        private readonly ILogger<CarrierController> _logger;

        public CarrierController(ICarrierBusinessBase<Carrier> carrierBusinessBase, ILogger<CarrierController> logger) : base(carrierBusinessBase, logger)
        {
            _carrierBusinessBase = carrierBusinessBase;
            _logger = logger;
        }
    }
}