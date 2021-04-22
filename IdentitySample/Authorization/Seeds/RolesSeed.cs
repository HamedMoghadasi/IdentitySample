using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authorization.Seeds
{
    public class RolesSeed : ISeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RolesSeed(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void Seed()
        {
            AddNewRole("admin");
            Console.WriteLine("Role Seed");
        }

        private void AddNewRole(string role)
        {
            var isExist = _roleManager.RoleExistsAsync(role).Result;
            if (!isExist)
            {
                var result = _roleManager.CreateAsync(new ApplicationRole { Name = role});
                
            }
        }
    }
}
