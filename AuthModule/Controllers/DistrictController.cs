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
    public class DistrictController : ActionBaseController<District>
    {

        public DistrictController(IBusinessBase<District> districtBusinessBase, ILogger<DistrictController> logger) : base(districtBusinessBase, logger)
        {
        }

        #region NonAction
        [NonAction]
        public override Task<IActionResult> Add([FromBody] District entity)
        {
            return base.Add(entity);
        }

        [NonAction]
        public override Task<IActionResult> Update([FromBody] District entity)
        {
            return base.Update(entity);
        }

        [NonAction]
        public override Task<IActionResult> Remove([FromBody] int Id)
        {
            return base.Remove(Id);
        }

        [NonAction]
        public override Task<IActionResult> Remove([FromBody] District model)
        {
            return base.Remove(model);
        }

        [NonAction]
        public override Task<IActionResult> Get([FromBody] District entity)
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