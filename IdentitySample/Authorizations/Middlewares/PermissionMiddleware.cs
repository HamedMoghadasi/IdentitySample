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

        public async Task InvokeAsync(HttpContext httpContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext _context)
        {

            var hasClaims = new List<bool>();
            var targetPersistedClaims = new List<Claims>();
            var controllerActionDescriptor = httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();

            if (controllerActionDescriptor != null)
            {
                var controllerName = $"{controllerActionDescriptor.ControllerName}Controller";
                var actionName = controllerActionDescriptor.ActionName;
                var actionsClaims = _context.Claims.Where( i=> i.ControllerName == controllerName && i.ActionName == actionName)
                    .AsNoTracking()
                    .AsEnumerable();
                if (actionsClaims.Count() > 0 && httpContext.User.Identity.IsAuthenticated) {
                    var userName = httpContext.User.Identity.Name;
                    var user = await userManager.FindByNameAsync(userName);
                    var roles = await userManager.GetRolesAsync(user);

                    foreach (var role in roles)
                    {
                        var identityRole = await roleManager.FindByNameAsync(role);
                        var roleClaims = await roleManager.GetClaimsAsync(identityRole);
                        var actionsClaimsCount = actionsClaims.Count();
                        var validClaims = roleClaims.Where(r => actionsClaims.Any(ac => ac.ClaimType == r.Type && ac.ClaimValue == r.Value));
                        hasClaims.Add(validClaims.Count() == actionsClaimsCount);
                    }

                    if (!hasClaims.Any(i => i == true))
                    {
                        httpContext.Response.Redirect("/Identity/Account/AccessDenied");
                    }
                }

                



            }
            //var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            //// Redirect to login if user is not authenticated. This instruction is neccessary for JS async calls, otherwise everycall will return unauthorized without explaining why
            //if (!isAuthenticated && httpContext.Request.Path.Value != "/Identity/Account/Login")
            //{
            //    httpContext.Response.Clear();
            //    httpContext.Response.Redirect("/Identity/Account/Login");
            //}
            //else if(isAuthenticated)
            //{
            //    var controllerActionDescriptor = httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();

            //    if (controllerActionDescriptor != null) {
            //        var controllerName = $"{controllerActionDescriptor.ControllerName}Controller";
            //        var actionName = controllerActionDescriptor.ActionName;

            //        var userName = httpContext.User.Identity.Name;
            //        var user = userManager.FindByNameAsync(userName).Result;
            //        var roles = userManager.GetRolesAsync(user).Result;

            //        foreach (var role in roles)
            //        {
            //            var identityRole = roleManager.FindByNameAsync(role).Result;
            //            var claims = roleManager.GetClaimsAsync(identityRole).Result;
            //            hasClaims.Add(claims.Any(c => c.Type == GlobalClaimsType.Permission && c.Value == $"{controllerName}.{actionName}"));
            //        }

            //        if (!hasClaims.Any(i => i == true))
            //        {
            //            httpContext.Response.Redirect("/Identity/Account/AccessDenied");
            //        }
            //    }


            //}
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
}
