using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DynamicAuthorization.Models
{
    public class Claims: ModelBase
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string DisplayName { get; set; }
    }

    public class ClaimsComparer : IEqualityComparer<Claims>
    {
        public bool Equals(Claims x, Claims y)
        {
            return x.ClaimType == y.ClaimType && x.ClaimValue == y.ClaimValue;
        }

        public int GetHashCode([DisallowNull] Claims obj)
        {
            return obj.ClaimType.GetHashCode() & obj.ClaimValue.GetHashCode();
        }
    }

    public class ClaimsConfiguration : IEntityTypeConfiguration<Claims>
    {
        public void Configure(EntityTypeBuilder<Claims> builder)
        {
            builder
            .HasIndex(p => new { p.ClaimType, p.ClaimValue, p.ControllerName, p.ActionName, p.DisplayName }).IsUnique();

            builder
           .Property(p => p.ActionName)
           .HasDefaultValue(string.Empty);

            builder
          .Property(p => p.ControllerName)
          .HasDefaultValue(string.Empty);

           builder
          .Property(p => p.ClaimType)
          .HasDefaultValue(string.Empty);

            builder
              .Property(p => p.ClaimValue)
              .HasDefaultValue(string.Empty);
            
            builder
              .Property(p => p.DisplayName)
              .HasDefaultValue(string.Empty);
        }
    }
}
