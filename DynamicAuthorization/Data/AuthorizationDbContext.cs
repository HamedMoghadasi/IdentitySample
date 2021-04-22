using DynamicAuthorization.Core;
using DynamicAuthorization.Extensions;
using DynamicAuthorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DynamicAuthorization.Data
{
    public class AuthorizationDbContext<TDbContext>: IdentityDbContext<ApplicationUser,ApplicationRole,Guid,IdentityUserClaim<Guid>,ApplicationUserRole,IdentityUserLogin<Guid>, ApplicationRoleClaim,IdentityUserToken<Guid>>, IAuthorizationDbContext
        where TDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions<TDbContext> options)
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
