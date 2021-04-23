using IdentitySample.Data;
using Authorization.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Models;
using Authorization.Rpositories;

namespace Authorization.Filters.Security
{
    public class Security : ISecurity
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClaimsRepository _claimsRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;

        public Security(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor, IClaimsRepository claimsRepository, IRoleClaimRepository roleClaimRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _claimsRepository = claimsRepository;
            _roleClaimRepository = roleClaimRepository;
        }
        public async Task<bool> IsGrantedAsync(string claimType, string claimValue)
        {
            var isClaimExistedInDb = _claimsRepository.GetAll().Any(i => i.ClaimType == claimType && i.ClaimValue == claimValue);
            if (!isClaimExistedInDb)
            {
                return false;
            }
            var result = false;
            var hasClaims = new List<bool>();
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    var identityRole = await _roleManager.FindByNameAsync(role);
                    var claims = _roleClaimRepository.GetClaims(identityRole);
                    hasClaims.Add(claims.Any(c => c.ClaimType == claimType && c.ClaimValue == claimValue));
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

        public async Task<bool> IsGrantedAsync(string roleName)
        {
            var result = false;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                result = await _userManager.IsInRoleAsync(user, roleName);
            }
            return result;
        }
    }
}
