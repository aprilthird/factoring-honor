using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Extensions;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.Extensions;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Authorize(Roles = ConstantHelpers.Role.CUSTOMER)]
    [Route("recibos")]
    public class FeeReceiptController : BaseController
    {
        public FeeReceiptController(FactoringHonorDbContext context,
            UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var user = await _context.Users
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            var fees = await _context.FeeReceipts
                .Include(x => x.CurrencyType)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.DiscountDate)
                .ToListAsync();
            var model = new EffectiveCostSummaryViewModel()
            {
                Details = new List<EffectiveCostRateDetailViewModel>()
            };
            if (!fees.Any())
                return View(model);
            var minDate = fees[0].DiscountDate;
            var maxDate = fees.Max(x => x.PaymentDate);
            model.Details = Enumerable.Range(0, (maxDate - minDate).Days + 1)
                            .Select(x => new EffectiveCostRateDetailViewModel
                            {
                                TermNumber = x + 1,
                                Date = minDate.AddDays(x).ToLocalDateFormat(),
                                Value = fees.Where(f => f.PaymentDate == minDate.AddDays(x))
                                        .Sum(f => f.DeliveredAmmount.ToCurrencyType(f.CurrencyType, user.CurrencyType))
                                        - fees.Where(f => f.DiscountDate == minDate.AddDays(x)) 
                                        .Sum(f => f.ReceivedAmmount.ToCurrencyType(f.CurrencyType, user.CurrencyType))
                            });
            var points = model.Details.Select(x => x.Value).ToArray();
            model.Rate = Math.Pow(1 + Financial.IRR(ref points, 0.00000001  ), ConstantHelpers.Bank.COMMERCIAL_YEAR) - 1;
            return View(model);
        }

        [HttpGet("nuevo")]
        public IActionResult New() => View();

        [HttpGet("listar")]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var user = await _context.Users
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            var query = _context.FeeReceipts
                .Include(x => x.CurrencyType)
                .OrderBy(x => x.CreateDate)
                .Where(x => x.UserId == userId)
                .Select(x => x.ToCurrencyType(user.CurrencyType).ToViewModel())
                .AsQueryable();
            var data = await query.ToListAsync();
            data.ForEach(x => x.Number = data.IndexOf(x) + 1);
            return Ok(data);
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> Calculate(FeeReceiptViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var issueDate = model.IssueDate.ToUtcDateTime();
            var discountDate = model.DiscountDate.ToUtcDateTime();
            var paymentDate = model.PaymentDate.ToUtcDateTime();
            if (issueDate > discountDate || issueDate > paymentDate)
                return BadRequest("Fecha de Emisión inválida.");
            if (discountDate > paymentDate)
                return BadRequest("Fecha de Descuento inválida.");
            var feeReceipt = await PrepareFeeReceipt(model);
            return Ok(feeReceipt.ToViewModel());
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateFee(FeeReceiptViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var issueDate = model.IssueDate.ToUtcDateTime();
            var discountDate = model.DiscountDate.ToUtcDateTime();
            var paymentDate = model.PaymentDate.ToUtcDateTime();
            if (issueDate > discountDate || issueDate > paymentDate)
                return BadRequest("Fecha de Emisión inválida.");
            if (discountDate > paymentDate)
                return BadRequest("Fecha de Descuento inválida.");
            var feeReceipt = await PrepareFeeReceipt(model);
            await _context.FeeReceipts.AddAsync(feeReceipt);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteFee(Guid id)
        {
            var feeReceipt = await _context.FeeReceipts.FindAsync(id);
            _context.FeeReceipts.Remove(feeReceipt);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [NonAction]
        public async Task<FeeReceipt> PrepareFeeReceipt(FeeReceiptViewModel model)
        {
            var bank = await _context.Banks
                .Include(x => x.BankCosts)
                .ThenInclude(x => x.CurrencyType)
                .Where(x => x.Id == model.BankId)
                .FirstOrDefaultAsync();
            var userId = GetUserId();
            var user = await _context.Users
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            return model.ToModel(bank, user);
        }
    }
}