using Authorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Extensions
{
    public static class ModelConfigurationsExtensions
    {
        public static void ApplyPermissionModelsConfiguration(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ModelBaseConfiguration<Claims>(new ClaimsConfiguration()));
            builder.ApplyConfiguration(new ModelBaseConfiguration<Domain>());
            builder.ApplyConfiguration(new ModelBaseConfiguration<RoleClaim>(new RoleClaimConfiguration()));

            builder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        }
    }
}
