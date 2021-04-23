using Authorizations.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using Authorization.Models;
using Authorization.Data;
using Authorization.Rpositories;

namespace Authorization.Seeds
{
    public class RoleClaimsSeed: ISeed
    {
        private readonly IClaimsRepository _claimsRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleClaimsSeed(RoleManager<ApplicationRole> roleManager, IClaimsRepository claimsRepository, IRoleClaimRepository roleClaimRepository)
        {
            _roleManager = roleManager;
            _claimsRepository = claimsRepository;
            _roleClaimRepository = roleClaimRepository;
        }
        public double ExecutionOrder => 5;


        public void Seed()
        {
            var claims =_claimsRepository.GetAll().ToList();
            var adminRole = _roleManager.FindByNameAsync(SeedOptions.DefaultAdmin).Result;
            var adminClaims = _roleClaimRepository.GetClaims(adminRole);

            claims.ForEach(item =>
            {
                if (!adminClaims.Any(i => item.ClaimType == i.ClaimType && item.ClaimValue == i.ClaimValue)) {
                    _roleClaimRepository.Add(new RoleClaim { ClaimId = item.Id, RoleId= adminRole.Id});
                }
            });
            _roleClaimRepository.SaveChanges();

            Console.WriteLine("Role Claims Seed");
        }

    }
}
