using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FactoringHonorDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public BaseController(FactoringHonorDbContext context)
        {
            _context = context;
        }

        public BaseController(FactoringHonorDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public BaseController(FactoringHonorDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected virtual async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
        protected virtual string GetUserId() => _userManager.GetUserId(User);
    }
}