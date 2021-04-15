using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Filters.RazorPermission
{
    public class RazorPermission : IRazorPermission
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RazorPermission(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsGranted(string claimType, string claimValue)
        {
            var result = false;
            var hasClaims = new List<bool>();

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                var roles = _userManager.GetRolesAsync(user).Result;

                foreach (var role in roles)
                {
                    var identityRole = _roleManager.FindByNameAsync(role).Result;
                    var claims = _roleManager.GetClaimsAsync(identityRole).Result;
                    hasClaims.Add(claims.Any(c => c.Type == claimType && c.Value == claimValue));
                }

            if (hasClaims.Any(i => i == true))
            {
                result = true;
            }
            else {
                result = false;
            }

                return result;
            
        }
    }
}
