using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Customer.Register.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Customer.Register.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ClaimsPrincipal claims = ClaimsPrincipal.Current;
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
