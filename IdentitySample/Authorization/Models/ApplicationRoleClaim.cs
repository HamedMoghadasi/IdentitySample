using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        //[ForeignKey(nameof(RoleId))]
        //public virtual ApplicationRole Role { get; set; }

        //public Guid ClaimId { get; set; }
        //public virtual Claims Claim { get; set; }
    }

    public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.HasKey(i => i.Id);

           // builder
           //.HasOne(i => i.Claim)
           //.WithMany(i => i.ApplicationRoleClaims)
           //.HasForeignKey(i => i.ClaimId);

           // builder
           //.HasOne(i => i.Role)
           //.WithMany(i => i.ApplicationRoleClaims)
           //.HasForeignKey(i => i.RoleId);
        }
    }
}
