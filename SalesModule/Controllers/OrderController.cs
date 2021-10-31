using AuthModule.Interfaces;
using BaseModule.Controllers;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;
using SalesModule.Dtos;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [ApiController]
    public class OrderController : ActionBaseController<Order>
    {
        private readonly IOrderBusinessBase<Order> _orderBusinessBase;

        public OrderController(IOrderBusinessBase<Order> orderBusinessBase, ILogger<OrderController> logger) : base(orderBusinessBase, logger)
        {
            _orderBusinessBase = orderBusinessBase;
        }

        [HttpPost("GetStoreOrders")]
        public async Task<IActionResult> GetStoreOrders(PagingRequestEntity<StoreOrderViewModel> request)
        {
            var result = await _orderBusinessBase.StoreOrders(request);
            return Ok(result);
        }
    }
}