using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Authorizations.Extensions
{
    public static class RoleManagerExtensions
    {
        public static void AddClaims(this RoleManager<IdentityRole> roleManager,Claims claims)
        {
            Console.WriteLine("add claims");
        }
    }
}
