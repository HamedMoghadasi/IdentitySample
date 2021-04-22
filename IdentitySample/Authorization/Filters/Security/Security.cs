﻿using IdentitySample.Data;
using Authorization.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Models;

namespace Authorization.Filters.Security
{
    public class Security : ISecurity
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public Security(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<bool> IsGrantedAsync(string claimType, string claimValue)
        {
            var isClaimExistedInDb = _context.Claims.Any(i => i.ClaimType == claimType && i.ClaimValue == claimValue);
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
                    var claims = await _roleManager.GetClaimsAsync(identityRole);
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