﻿using BaseModule.Controllers;
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

        [HttpPost("AddProductToStore")]
        public async Task<IActionResult> AddProductToStore([FromBody] AddProductToVendorRequest request)
        {
            var result = await _productService.AddProductToStore(request);
            return Ok(result);
        }
    }
}
