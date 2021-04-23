using Authorization.Extensions;
using Authorization.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Data
{
    public class AuthorizationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public AuthorizationDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Claims> Auth_Claims { get; set; }
        public DbSet<Domain> Auth_Domains{ get; set; }
        public DbSet<RoleClaim> Auth_RoleClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyPermissionModelsConfiguration();
            base.OnModelCreating(builder);
        }
    }
}

