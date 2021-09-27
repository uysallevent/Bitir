using AuthModule.Business;
using AuthModule.Dto;
using AuthModule.Entities;
using AuthModule.Interfaces;
using BaseModule.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [ApiController]
    public class AuthBaseController : ActionBaseController<UserAccount>
    {
        private readonly IAuthBusinessBase<UserAccount> _authBusinessBase;
        private readonly ILogger<AuthBaseController> _logger;

        public AuthBaseController(IAuthBusinessBase<UserAccount> authBusinessBase, ILogger<AuthBaseController> logger) : base(authBusinessBase, logger)
        {
            _authBusinessBase = authBusinessBase;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authBusinessBase.Login(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message ?? "While login operation, error occurred");
                return BadRequest(ex.Message ?? "While login operation, error occurred");
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserAccount userAccount)
        {
            try
            {
                var result = await _authBusinessBase.Register(userAccount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message ?? "While login operation, error occurred");
                return BadRequest(ex.Message ?? "While login operation, error occurred");
            }
        }

        [NonAction]
        public override Task<IActionResult> Update([FromBody] UserAccount entity)
        {
            return base.Update(entity);
        }

        [NonAction]
        public override Task<IActionResult> GetAll([FromBody] UserAccount entity = null)
        {
            return base.GetAll(entity);
        }

        [NonAction]
        public override Task<IActionResult> Add([FromBody] UserAccount entity)
        {
            return base.Add(entity);
        }
    }
}