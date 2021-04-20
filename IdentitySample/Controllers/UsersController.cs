using IdentitySample.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<IdentityUser> Get()
        {
            return _userManager.Users.AsEnumerable();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest assignRoleRequest)
        {
            try
            {
                var theUser = await _userManager.FindByIdAsync(assignRoleRequest.UserId);
                var allRoles = _roleManager.Roles.AsEnumerable().ToList();
                var selectedRoles = assignRoleRequest.Roles;

                var notSelectedRoles = allRoles.Select(role => role.Id).Except(selectedRoles).ToList();

                foreach (var roleId in selectedRoles)
                {
                    var theRole = await _roleManager.FindByIdAsync(roleId);
                    if (theUser != null && theRole != null)
                    {
                        var IsUserInTheRole = await _userManager.IsInRoleAsync(theUser, theRole.Name);
                        if (!IsUserInTheRole)
                        {
                            await _userManager.AddToRoleAsync(theUser, theRole.Name);
                        }
                    }
                }

                foreach (var roleId in notSelectedRoles)
                {
                    var theRole = await _roleManager.FindByIdAsync(roleId);
                    if (theUser != null && theRole != null)
                    {
                        var IsUserInTheRole = await _userManager.IsInRoleAsync(theUser, theRole.Name);
                        if (IsUserInTheRole)
                        {
                            await _userManager.RemoveFromRoleAsync(theUser, theRole.Name);
                        }
                    }
                }
                return Ok("Successful!");
            }
            catch (Exception)
            {
                return BadRequest("500");
                throw;
            }
        }
    }
}
