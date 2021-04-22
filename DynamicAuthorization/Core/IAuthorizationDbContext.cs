using DynamicAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicAuthorization.Core
{
    public interface IAuthorizationDbContext
    {
        DbSet<Claims> Claims { get; set; }
        DbSet<Domain> Domains{ get; set; }
    }
}
