using Authorization.Models;
using IdentitySample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Authorization.Stores
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, Guid, ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer) { }

        //protected override ApplicationRoleClaim CreateRoleClaim(ApplicationRole role, Claim claim)
        //{
        //    return new ApplicationRoleClaim
        //    {
        //        RoleId = role.Id,
        //        ClaimType = claim.Type,
        //        ClaimValue = claim.Value
        //    };
        //}
    }
}
