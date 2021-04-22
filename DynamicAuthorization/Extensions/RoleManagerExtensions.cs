using DynamicAuthorization.Models;
using Microsoft.AspNetCore.Identity;
using System;

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
