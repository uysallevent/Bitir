using AuthModule.Business;
using AuthModule.Dto;
using AuthModule.Entities;
using AuthModule.Interfaces;
using BaseModule.Controllers;
using BaseModule.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [ApiController]
    public class AccountTypeController : ActionBaseController<AccountType>
    {
        private readonly IBusinessBase<AccountType> _accountTypeBusinessBase;
        private readonly ILogger<AccountTypeController> _logger;

        public AccountTypeController(IBusinessBase<AccountType> accountTypeBusinessBase, ILogger<AccountTypeController> logger) : base(accountTypeBusinessBase, logger)
        {
            _accountTypeBusinessBase = accountTypeBusinessBase;
            _logger = logger;
        }

        [AllowAnonymous]
        public override Task<IActionResult> GetAll([FromBody] AccountType entity = null)
        {
            return base.GetAll(entity);
        }
    }
}