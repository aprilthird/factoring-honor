using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FACTORINGHONOR.PE.WEB.Services
{
    public class UserConfigurationService : IUserConfigurationService
    {
        private readonly FactoringHonorDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserConfigurationService(FactoringHonorDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserConfigurationViewModel> GetUserConfiguration(ClaimsPrincipal userClaim)
        {
            var user = await _userManager.GetUserAsync(userClaim);
            var model = await _context.Users
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == user.Id)
                .Select(x => new UserConfigurationViewModel
                {
                    RateType = x.RateType,
                    CurrencyTypeId = x.CurrencyTypeId,
                    CurrencyType = new CurrencyTypeViewModel
                    {
                        Name = x.CurrencyType.Name,
                        ShortName = x.CurrencyType.ShortName,
                        Symbol = x.CurrencyType.Symbol,
                        Value = x.CurrencyType.Value
                    }
                }).FirstOrDefaultAsync();
            return model;
        }
    }
}
