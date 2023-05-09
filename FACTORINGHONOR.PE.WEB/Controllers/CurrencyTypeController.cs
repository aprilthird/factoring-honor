using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Authorize]
    [Route("tipos-de-cambio")]
    public class CurrencyTypeController : BaseController
    {
        public CurrencyTypeController(FactoringHonorDbContext context) 
            : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.CurrencyTypes
                .Select(x => new CurrencyTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortName = x.ShortName,
                    Symbol = x.Symbol,
                    Value = x.Value
                }).ToListAsync();
            return Ok(data);
        }
    }
}