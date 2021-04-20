using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Authorization.Filters
{
    public class Permission : TypeFilterAttribute
    {
        private string _claimValue;
        private string _claimType;

        public Permission(string claimType , string claimValue) : base(typeof(PermissionFilter))
        {
            _claimValue = claimValue;
            _claimType = claimType;
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }

        public string claimValue { get { return _claimValue; } private set { _claimValue = value; } }
        public string claimType { get { return _claimType; } private set { _claimType = value; } }
    }

    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionFilter(Claim claim, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _claim = claim;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaims = new List<bool>();
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                context.Result = new RedirectResult("Identity/Account/Login");
            }
            else {
                var userName = context.HttpContext.User.Identity.Name;
                var user =  _userManager.FindByNameAsync(userName).Result;
                var roles =  _userManager.GetRolesAsync(user).Result;

                foreach (var role in roles)
                {
                    var identityRole =  _roleManager.FindByNameAsync(role).Result;
                    var claims =  _roleManager.GetClaimsAsync(identityRole).Result;
                    hasClaims.Add(claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value));
                }

                if (!hasClaims.Any(i => i == true))
                {
                    context.Result = new ForbidResult();   
                }
            }
        }
    }
}
