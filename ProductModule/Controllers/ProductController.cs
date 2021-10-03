using BaseModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductModule.Entities;
using ProductModule.Interfaces;
using System.Threading.Tasks;

namespace ProductModule.Controllers
{
    [ApiController]
    public class ProductController : ActionBaseController<Product>
    {
        private readonly IProductBusinessBase<Product> _productBusinessBase;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductBusinessBase<Product> productBusinessBase, ILogger<ProductController> logger) : base(productBusinessBase, logger)
        {
            _productBusinessBase = productBusinessBase;
            _logger = logger;
        }

        [HttpPost("GetSystemProducts")]
        public async Task<IActionResult> GetSystemProducts()
        {
            var result = await _productBusinessBase.GetSystemProducts();
            return Ok(result);
        }
    }
}
