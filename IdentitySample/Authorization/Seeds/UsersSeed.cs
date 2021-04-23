using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authorization.Seeds
{
    public class UsersSeed : ISeed
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public double ExecutionOrder => 3;
        public void Seed()
        {
            AddNewUser(SeedOptions.DefaultUser, SeedOptions.DefaultPassword, SeedOptions.DefaultAdmin);
            Console.WriteLine("user seed");
        }

        private void AddNewUser(ApplicationUser theUser, string password, string role)
        {
            var isExist = _userManager.FindByNameAsync(theUser.Email).Result != null;
            if (!isExist)
            {
                var result = _userManager.CreateAsync(theUser, password).Result;
                if (result.Succeeded)
                {
                    var resultRole = _userManager.AddToRoleAsync(theUser, role).Result;
                }
            }
           
        }
    }
}
