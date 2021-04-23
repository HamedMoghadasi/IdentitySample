using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Seeds
{
    public static class SeedOptions
    {
        public static string DefaultAdmin = "superadmin";
        public static ApplicationUser DefaultUser = new ApplicationUser
        {
            Email = "superadmin@gmail.com",
            EmailConfirmed = true,
            UserName = "superadmin@gmail.com",
        };
        public static string DefaultPassword = "OrchidHM8632!";
        public static string DefaultDomain = "MobilityOne";
    }
}
