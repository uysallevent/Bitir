using AuthModule.Controllers;
using Bitir.Business.Interfaces;
using Bitir.Entity.TestDb.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Bitir.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ActionBaseController<Category>
    {
        private readonly ILogger _logger;
        private readonly ICategoryBusiness _categoryBusinessBase;
        public CategoryController(ICategoryBusiness categoryBusinessBase, ILogger logger) : base(categoryBusinessBase, logger)
        {
            _logger = logger;
            _categoryBusinessBase = categoryBusinessBase;
        }

        [HttpPost("AllCategoriesWithSub")]
        public virtual  IActionResult GetCategoriesWithSub()
        {
            try
            {
                var result =  _categoryBusinessBase.GetCategoriesWithSubItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories");
                return BadRequest("Error occurred while getting categories");
            }
        }
    }
}
