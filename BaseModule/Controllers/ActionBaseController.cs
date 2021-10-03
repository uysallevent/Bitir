using BaseModule.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BaseModule.Controllers
{
    [Route("[module]/[controller]")]
    [Authorize]
    public class ActionBaseController<T> : Controller
    where T : class, IEntity, new()
    {
        private readonly IBusinessBase<T> _businessBase;
        private ILogger _logger;

        public ActionBaseController(IBusinessBase<T> businessBase, ILogger logger)
        {
            _businessBase = businessBase;
            _logger = logger;
        }

        [HttpPost("GetByModel")]
        public virtual async Task<IActionResult> Get([FromBody] T entity)
        {
                var result = await _businessBase.GetAsync(entity);
                return Ok(result);
        }

        [HttpGet("GetById")]
        public virtual async Task<IActionResult> Find([FromQuery] int id)
        {
                var result = await _businessBase.FindAsync(id);
                return Ok(result);
        }

        [HttpPost("GetAll")]
        public virtual async Task<IActionResult> GetAll([FromBody] T entity = null)
        {
                var result = await _businessBase.GetAllAsync(entity);
                return Ok(result);
        }

        [HttpPost("Add")]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
                var result = await _businessBase.InsertAsync(entity);
                return Ok(result);
        }

        [HttpPut("Update")]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
                var result = await _businessBase.UpdateAsync(entity);
                return Ok(result);
        }

        [HttpDelete("RemoveByModel")]
        public virtual async Task<IActionResult> Remove([FromBody] T model)
        {
                var result = await _businessBase.DeleteAsync(model);
                return Ok(result);
        }

        [HttpDelete("RemoveById")]
        public virtual async Task<IActionResult> Remove([FromBody] int Id)
        {
                var result = await _businessBase.DeleteAsync(Id);
                return Ok(result);
        }

    }
}