﻿using AuthModule.Controllers;
using BaseModule.Interfaces;
using Bitir.Business.Interfaces;
using Bitir.Entity.TestDb.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bitir.Api.Controllers
{
    [Authorize]
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
    }
}
