using Authorization.Core;
using Authorization.Extensions;
using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentitySample.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claims> Claims { get; set; }
        public DbSet<Domain> Domains { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyPermissionModelsConfiguration();
            base.OnModelCreating(builder);
        }
    }
}
