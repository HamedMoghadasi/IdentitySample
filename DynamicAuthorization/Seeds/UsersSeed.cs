using Microsoft.AspNetCore.Identity;
using System;

namespace DynamicAuthorization.Seeds
{
    public class UsersSeed : ISeed
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersSeed(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void Seed()
        {
            AddNewUser(new IdentityUser
            {
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin@gmail.com",
            }, "orchidHM8632!", "admin");

            Console.WriteLine("user seed");
        }

        private void AddNewUser(IdentityUser theUser, string password, string role)
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
