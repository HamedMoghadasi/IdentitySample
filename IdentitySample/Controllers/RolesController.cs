using IdentitySample.Data;
using IdentitySample.DTOs.Roles;
using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> Get()
        {
            return _roleManager.Roles.AsEnumerable();
        }

        [HttpGet("GetRoleClaims")]
        public async Task<IEnumerable<GetRoleClaimsResponse>> GetRoleClaims([FromBody] GetRoleClaimsRequest role)
        {
            var identityRole = await _roleManager.FindByNameAsync(role.RoleName);
            var roleClaims = await _roleManager.GetClaimsAsync(identityRole);
            var allClaims = _context.Auth_Claims.ToList();
            var mappedRoleClaims = roleClaims.Select(item => new Claims
            {
                ClaimType = item.Type,
                ClaimValue = item.Value,
            }).ToList();

            var result = new List<GetRoleClaimsResponse>();
            allClaims.ForEach(item =>
            {
                var resultObjet = new GetRoleClaimsResponse() { Claims = item, Selected = false };
                if (mappedRoleClaims.Exists(i => i.ClaimType == item.ClaimType && i.ClaimValue == item.ClaimValue))
                {
                    resultObjet.Selected = true;
                }

                result.Add(resultObjet);
            });

            return result;
        }

        [HttpPost("HandleRoleClaim")]
        public async Task HandleRoleClaim([FromBody] RoleClaimRequest roleClaim)
        {
            var identityRole = await _roleManager.FindByNameAsync(roleClaim.RoleName);
            var roleClaims = await _roleManager.GetClaimsAsync(identityRole);
            if (roleClaims.Any(item => item.Type == roleClaim.ClaimType && item.Value == roleClaim.ClaimValue))
            {
                await _roleManager.RemoveClaimAsync(identityRole, new Claim(roleClaim.ClaimType, roleClaim.ClaimValue));
            }
            else
            {
                await _roleManager.AddClaimAsync(identityRole, new Claim(roleClaim.ClaimType, roleClaim.ClaimValue));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleRequest role)
        {
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (!isExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                return Ok(result);
            }
            return Ok("role existed!");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] string id, [FromBody] UpdateRoleRequest updatedRole)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
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
