using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}
