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
    public class ApplicationRole: IdentityRole<Guid>
    {
        [ForeignKey(nameof(Domain))]
        public Guid DomainId { get; set; }
        public virtual Domain Domain { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }

    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(x => x.Id);

            builder
            .HasOne(i => i.Domain)
            .WithMany(i => i.Roles)
            .HasForeignKey(i => i.DomainId);
        }
    }
}
