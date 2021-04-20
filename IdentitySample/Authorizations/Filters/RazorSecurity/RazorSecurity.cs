using IdentitySample.Data;
using Authorization.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Authorization.Filters.RazorSecurity
{
    public class RazorSecurity : ISecurity
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public RazorSecurity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public bool IsGranted(string claimType, string claimValue)
        {
            var isClaimExistedInDb = _context.Claims.Any(i => i.ClaimType == claimType && i.ClaimValue == claimValue);
            if (!isClaimExistedInDb) {
                return true;
            }
            var result = false;
            var hasClaims = new List<bool>();
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) {
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
                else
                {
                    result = false;
                }
            }
            

                return result;
            
        }

        public bool IsGranted(string claimValue)
        {
            return IsGranted(GlobalClaimsType.Permission, claimValue);
        }
    }
}
