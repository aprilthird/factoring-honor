using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Extensions;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Route("costos")]
    public class BankCostController : BaseController
    {
        public BankCostController(FactoringHonorDbContext context,
            UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAll(Guid? bankId = null)
        {
            var userId = GetUserId();
            var user = await _context.Users
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            var query = _context.BankCosts.AsQueryable();
            if (bankId.HasValue)
                query = query.Where(x => x.BankId == bankId.Value);
            var data = await query
                .Select(x => new BankCostViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Value = x.Value,
                    IsFinal = x.IsFinal,
                    ConvertedValue = x.Value.ToCurrencyType(x.CurrencyType, user.CurrencyType)
                }).ToListAsync();
            return Ok(data);
        }
    }
}