using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FACTORINGHONOR.PE.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using FACTORINGHONOR.PE.CORE.Helpers;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole(ConstantHelpers.Role.ADMIN))
                return RedirectToAction("Index", "Bank");
            else if (User.IsInRole(ConstantHelpers.Role.CUSTOMER))
                return RedirectToAction("Index", "FeeReceipt");
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult HowItWorks()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
