using IdentitySample.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentitySample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claims> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claims>()
            .HasIndex(p => new { p.ClaimType, p.ClaimValue,p.ControllerName,p.ActionName,p.DisplayName}).IsUnique();

            modelBuilder.Entity<Claims>()
           .Property(p => p.ActionName)
           .HasDefaultValue(string.Empty);

            modelBuilder.Entity<Claims>()
          .Property(p => p.ControllerName)
          .HasDefaultValue(string.Empty);

            modelBuilder.Entity<Claims>()
          .Property(p => p.ClaimType)
          .HasDefaultValue(string.Empty);

            modelBuilder.Entity<Claims>()
        .Property(p => p.ClaimValue)
        .HasDefaultValue(string.Empty);
            base.OnModelCreating(modelBuilder);
        }
    }
}
