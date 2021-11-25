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
            var result = await _carrierBusinessBase.AddCarrierToStoreAsync(request);
            return Ok(result);
        }

        [HttpPut("UpdateStoreCarrier")]
        public async Task<IActionResult> UpdateStoreCarrier([FromBody] UpdateCarrierToStoreRequest request)
        {
            var result = await _carrierBusinessBase.UpdateStoreCarrierAsync(request);
            return Ok(result);
        }

        [HttpPost("GetStoreCarriers")]
        public async Task<IActionResult> GetStoreCarriers()
        {
            var result = await _carrierBusinessBase.GetStoreCarriersAsync();
            return Ok(result);
        }

        [HttpPost("GetStoreCarriersByCarrierId")]
        public async Task<IActionResult> GetStoreCarriersByCarrierId(int carrierId)
        {
            var result = await _carrierBusinessBase.GetStoreCarriersAsync(carrierId);
            return Ok(result);
        }

        [HttpDelete("RemoveZoneFromCarrierById")]
        public async Task<IActionResult> RemoveZoneFromCarrierById(int carrierDistributionZoneId)
        {
            var result = await _carrierBusinessBase.RemoveZoneFromCarrier(carrierDistributionZoneId);
            return Ok(result);
        }
    }
}