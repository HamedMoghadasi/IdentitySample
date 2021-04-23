using Authorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class RoleClaim : ModelBase
    {
        [ForeignKey(nameof(Roles))]
        public Guid RoleId{ get; set; }
        public virtual ApplicationRole Roles { get; set; }

        [ForeignKey(nameof(Claims))]
        public Guid ClaimId { get; set; }
        public virtual Claims Claims { get; set; }
    }

    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder
           .HasOne(i => i.Claims)
           .WithMany(i => i.RoleClaims)
           .HasForeignKey(i => i.ClaimId);

           builder
          .HasOne(i => i.Roles)
          .WithMany(i => i.RoleClaims)
          .HasForeignKey(i => i.RoleId);
        }
    }
}
