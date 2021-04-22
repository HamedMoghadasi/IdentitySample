using Authorization.Models;
using Authorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Extensions
{
    public static class ModelConfigurations
    {
        public static void ApplyPermissionModelsConfiguration(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BaseModelConfigurations<Claims>(new ClaimsConfiguration()));
            builder.ApplyConfiguration(new BaseModelConfigurations<Domain>());
        }
    }

    public class BaseModelConfigurations<TModel> : IEntityTypeConfiguration<TModel> where TModel: ModelBase
    {
        private readonly IEntityTypeConfiguration<TModel> _configuration;

        public BaseModelConfigurations() { }
        public BaseModelConfigurations(IEntityTypeConfiguration<TModel> configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.ModifiedDate).HasDefaultValueSql("GETUTCDATE()");
            if (_configuration != null)
            { 
                _configuration.Configure(builder);
            }
        }
    }
}
