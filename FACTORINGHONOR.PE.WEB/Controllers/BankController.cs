using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Extensions;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Authorize]
    [Route("bancos")]
    public class BankController : BaseController
    {
        public BankController(FactoringHonorDbContext context,
            UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAll()
        {
            var user = await GetCurrentUserAsync();
            var banks = await _context.Banks.ToListAsync();
            var data = banks
                .Select(x => new BankViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortName = x.ShortName,
                    RateValue = x.RateValue,
                    RateTerm = x.RateTerm,
                    RateType = x.RateType,
                    ConvertedValue = x.RateValue.ToRate(x.RateType, user.RateType, x.RateTerm, ConstantHelpers.Bank.COMMERCIAL_YEAR)
                }).ToList();
            return Ok(data);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _context.Banks.FindAsync(id);
            var result = new BankViewModel
            {
                Id = data.Id,
                Name = data.Name,
                ShortName = data.ShortName,
                RateType = data.RateType,
                RateTerm = data.RateTerm,
                RateValue = data.RateValue
            };
            return Ok(result);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Create(BankViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bank = new Bank
            {
                Name = model.Name,
                ShortName = model.ShortName,
                RateType = model.RateType,
                RateTerm = model.RateTerm,
                RateValue = model.RateValue / 100
            };
            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BankViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bank = await _context.Banks.FindAsync(id);
            bank.Name = model.Name;
            bank.ShortName = model.ShortName;
            bank.RateType = model.RateType;
            bank.RateTerm = model.RateTerm;
            bank.RateValue = model.RateValue / 100;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);
            if (bank == null)
                return BadRequest($"Banco con Id '{id}' no encontrado.");
            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}