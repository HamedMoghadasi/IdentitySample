using Authorization.Filters;
using Authorization.Models;
using Authorization.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;


        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> _roleManager)
        {
            _logger = logger;
        }

        //[PermissionDisplayName("Access Inedx")]
        public IActionResult Index()
        {
            return View();
        }

        
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
