using BaseModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using ProductModule.Interfaces;
using System.Threading.Tasks;

namespace ProductModule.Controllers
{
    [ApiController]
    public class ProductController : ActionBaseController<Product>
    {
        private readonly IProductService<Product> _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(
            ILogger<ProductController> logger,
            IProductService<Product> productService) : base(productService, logger)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost("GetSystemProducts")]
        public async Task<IActionResult> GetSystemProducts()
        {
            var result = await _productService.GetSystemProducts();
            return Ok(result);
        }

        [HttpPost("GetStoreProducts")]
        public async Task<IActionResult> GetStoreProducts()
        {
            var result = await _productService.GetStoreProducts();
            return Ok(result);
        }

        [HttpPost("GetStoreProductsByCarrier")]
        public async Task<IActionResult> GetStoreProductsByCarrier([FromBody] StoreProdByCarrierRequest request)
        {
            var result = await _productService.GetStoreProductsByCarrier(request);
            return Ok(result);
        }

        [HttpPost("GetStoreProductsByStore")]
        public async Task<IActionResult> GetStoreProductsByStore()
        {
            var result = await _productService.GetStoreProductsByStore();
            return Ok(result);
        }

        [HttpPost("AddProductToStore")]
        public async Task<IActionResult> AddProductToStore([FromBody] AddProductToStoreRequest request)
        {
            var result = await _productService.AddProductToStore(request);
            return Ok(result);
        }

        [HttpPost("AddOrUpdateProductInCarrier")]
        public async Task<IActionResult> AddOrUpdateProductInCarrier([FromBody] UpdateProductStoreRequest request)
        {
            var result = await _productService.StoreProductAddOrUpdateInCarrier(request);
            return Ok(result);
        }

        [HttpPost("StoreProductUpdate")]
        public async Task<IActionResult> StoreProductUpdate([FromBody] UpdateProductStoreRequest request)
        {
            var result = await _productService.StoreProductUpdate(request);
            return Ok(result);
        }

        [HttpPost("StoreProductRemoveFromCarrier")]
        public async Task<IActionResult> StoreProductRemoveFromCarrier([FromBody] UpdateProductStoreRequest request)
        {
            var result = await _productService.StoreProductRemoveFromCarrier(request);
            return Ok(result);
        }

        [HttpDelete("StoreProductRemoveFromStore")]
        public async Task<IActionResult> StoreProductRemoveFromStore([FromBody] int storeProductId)
        {
            var result = await _productService.StoreProductRemoveFromStore(storeProductId);
            return Ok(result);
        }
    }
}
