using Authorizations.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class ModelBase<TKey>: IModelBase
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ModelBase : ModelBase<Guid> { }


    public class ModelBaseConfiguration<TModel> : IEntityTypeConfiguration<TModel> where TModel : ModelBase
    {
        private readonly IEntityTypeConfiguration<TModel> _configuration;
        public ModelBaseConfiguration() { }

        public ModelBaseConfiguration(IEntityTypeConfiguration<TModel> configuration)
        {
            _configuration = configuration;
        }
        public void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("GETUTCDATE()");
            if (_configuration != null) 
            {
                _configuration.Configure(builder);
            }
        }
    }
}
