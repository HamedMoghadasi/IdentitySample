using Authorization.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Extensions
{
    public static class ModelConfigurationExtenstion
    {
        public static void ApplyAuthorizationModelConfiguration(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ModelBaseConfiguration<Claims>(new ClaimsConfiguration()));
            builder.ApplyConfiguration(new ModelBaseConfiguration<Domain>());
        }
    }
}
