using IdentitySample.DTOs.Roles;
using Microsoft.AspNetCore.Http;
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
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> Get()
        {
            return _roleManager.Roles.AsEnumerable();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleRequest role)
        {
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (!isExist) {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                return Ok(result);
            }
            return Ok("role existed!");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] string id, [FromBody] UpdateRoleRequest updatedRole)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) {
                role.Name = updatedRole.Name;
                var result = await _roleManager.UpdateAsync(role);
                return Ok(result);
            }
            return Ok("role not existed!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return Ok(result);
            }
            return Ok("role not existed!");
        }
    }
}
