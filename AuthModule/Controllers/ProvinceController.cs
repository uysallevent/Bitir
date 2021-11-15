using AuthModule.Dto;
using AuthModule.Interfaces;
using BaseModule.Controllers;
using BaseModule.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.AuthModuleEntities;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [ApiController]
    public class ProvinceController : ActionBaseController<Province>
    {

        public ProvinceController(IBusinessBase<Province> provinceBusinessBase, ILogger<ProvinceController> logger) : base(provinceBusinessBase, logger)
        {
        }


        #region NonAction
        [NonAction]
        public override Task<IActionResult> Add([FromBody] Province entity)
        {
            return base.Add(entity);
        }

        [NonAction]
        public override Task<IActionResult> Update([FromBody] Province entity)
        {
            return base.Update(entity);
        }

        [NonAction]
        public override Task<IActionResult> Remove([FromBody] int Id)
        {
            return base.Remove(Id);
        }

        [NonAction]
        public override Task<IActionResult> Remove([FromBody] Province model)
        {
            return base.Remove(model);
        }

        [NonAction]
        public override Task<IActionResult> Get([FromBody] Province entity)
        {
            return base.Get(entity);
        }

        [NonAction]
        public override Task<IActionResult> Find([FromQuery] int id)
        {
            return base.Find(id);
        }
        #endregion
    }
}