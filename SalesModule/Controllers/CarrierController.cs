using AuthModule.Interfaces;
using BaseModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System.Threading.Tasks;

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

        [HttpPost("AddCarrierToStore")]
        public async Task<IActionResult> AddCarrierToStore([FromBody] AddCarrierToStoreRequest request)
        {
            var result = await _carrierBusinessBase.AddCarrierToStore(request);
            return Ok(result);
        }

        [HttpPost("UpdateStoreCarrier")]
        public async Task<IActionResult> UpdateStoreCarrier([FromBody] UpdateCarrierToStoreRequest request)
        {
            var result = await _carrierBusinessBase.UpdateStoreCarrier(request);
            return Ok(result);
        }

        [HttpPost("GetStoreCarriers")]
        public async Task<IActionResult> GetStoreCarriers()
        {
            var result = await _carrierBusinessBase.GetStoreCarriers();
            return Ok(result);
        }
    }
}