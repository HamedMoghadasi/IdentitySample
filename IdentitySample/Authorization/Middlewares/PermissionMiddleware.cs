using IdentitySample.Data;
using Authorization.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Constants;
using Authorization.Data;
using Authorization.Rpositories;

namespace Authorization.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IClaimsRepository claimsRepository,IRoleClaimRepository roleClaimRepository)
        {
            if (httpContext.GetEndpoint() != null) {
                var hasClaims = new List<bool>();
                var targetPersistedClaims = new List<Claims>();
                var controllerActionDescriptor = httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();

                if (controllerActionDescriptor != null)
                {
                    var controllerName = $"{controllerActionDescriptor.ControllerName}Controller";
                    var actionName = controllerActionDescriptor.ActionName;

                    var actionsClaims = claimsRepository.Find(i => i.ControllerName == controllerName && i.ActionName == actionName && i.ClaimType == GlobalClaimsType.Permission);

                    if (actionsClaims.Count() > 0 && httpContext.User.Identity.IsAuthenticated)
                    {
                        var userName = httpContext.User.Identity.Name;
                        var user = await userManager.FindByNameAsync(userName);
                        var roles = await userManager.GetRolesAsync(user);

                        foreach (var role in roles)
                        {
                            var identityRole = await roleManager.FindByNameAsync(role);
                            var roleClaims = roleClaimRepository.GetClaims(identityRole);
                            roleClaims = roleClaims.Where(i => i.ClaimType == GlobalClaimsType.Permission).ToList();
                            var actionsClaimsCount = actionsClaims.Count();
                            var validClaims = roleClaims.Where(r => actionsClaims.Any(ac => ac.ClaimType == r.ClaimType && ac.ClaimValue == r.ClaimValue));
                            hasClaims.Add(validClaims.Count() == actionsClaimsCount);
                        }

                        if (!hasClaims.Any(i => i == true))
                        {
                            httpContext.Response.Redirect("/Identity/Account/AccessDenied");
                        }
                    }
                }
            }
           
            await _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionMiddleware>();
        }
    }
    public class Response {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
