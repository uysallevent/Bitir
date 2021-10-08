using AuthModule.Dto;
using AuthModule.Interfaces;
using BaseModule.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.AuthModuleEntities;
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

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authBusinessBase.Login(loginDto);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserAccount userAccount)
        {
            var result = await _authBusinessBase.Register(userAccount);
            return Ok(result);
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