using IdentitySample.Filters;
using IdentitySample.Models;
using IdentitySample.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Controllers
{
    //[Permission(GlobalClaimsType.Permission, GlobalClaimsValue.AccessHome)]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [PermissionDisplayName("Access Inedx")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [RazorPermission(GlobalClaimsType.Permission, RazorClaimsValue.AccessParagraph)]
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
