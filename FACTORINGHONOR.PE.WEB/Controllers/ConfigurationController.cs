using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Authorize]
    [Route("configuracion")]
    public class ConfigurationController : BaseController
    {
        public ConfigurationController(FactoringHonorDbContext context, 
            UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> Update(UserConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await GetCurrentUserAsync();
            user.RateType = model.RateType;
            user.CurrencyTypeId = model.CurrencyTypeId;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}