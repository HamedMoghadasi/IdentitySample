using IdentitySample.Authorizations.Extensions;
using IdentitySample.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using Authorization.Models;

namespace Authorization.Seeds
{
    public class RoleClaimsSeed: ISeed
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleClaimsSeed(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public void Seed()
        {
            var claims = _context.Claims.Select(item => new Claim(item.ClaimType, item.ClaimValue)).ToList();
            var adminRole = _roleManager.FindByNameAsync("admin").Result;
            var adminClaims = _roleManager.GetClaimsAsync(adminRole).Result;

            claims.ForEach(item =>
            {
                if (!adminClaims.Any(i => item.Type == i.Type && item.Value == i.Value)) { 
                    var result = _roleManager.AddClaimAsync(adminRole, item).Result;
                }
            });

            Console.WriteLine("Role Claims Seed");
        }

    }
}
