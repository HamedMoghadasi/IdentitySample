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

        public void Seed()
        {
            AddNewUser(new ApplicationUser
            {
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin@gmail.com",
            }, "orchidHM8632!", "admin");

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
