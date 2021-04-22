using Authorization.Extensions;
using Authorization.Models;
using IdentitySample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Core
{
    public interface IAuthorizationDbContext
    {
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Domain> Domains { get; set; }
    }
}
