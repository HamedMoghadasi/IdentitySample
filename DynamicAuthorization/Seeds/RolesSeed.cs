﻿using Microsoft.AspNetCore.Identity;
using System;

namespace DynamicAuthorization.Seeds
{
    public class RolesSeed : ISeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesSeed(RoleManager<IdentityRole> roleManager)
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
                var result = _roleManager.CreateAsync(new IdentityRole(role));
                
            }
        }
    }
}