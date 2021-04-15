using IdentitySample.Filters;
using IdentitySample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentitySample.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ClaimsDetector
    {
        private readonly RequestDelegate _next;

        public ClaimsDetector(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var claims = new List<Claims>();

            var controllers = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(i => typeof(ControllerBase).IsAssignableFrom(i))
                .Select(i => i);

            FindControllersClaims(claims, controllers);
            FindActionsClaims(claims, controllers);



            return _next(httpContext);
        }

        private static void FindActionsClaims(List<Claims> claims, IEnumerable<Type> controllers)
        {
            foreach (Type controller in controllers)
            {
                var actions = controller.GetMethods().Where(i => i.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && i.CustomAttributes.Count() > 0).ToList();
                foreach (var action in actions)
                {
                    var permissionAttributes = action.GetCustomAttributes<Permission>(false);
                    foreach (var attr in permissionAttributes)
                    {
                        claims.Add(new Claims
                        {
                            ActionName = action.Name,
                            ControllerName = action.DeclaringType.Name,
                            ClaimValue = attr.claimValue,
                            ClaimType = attr.claimType,
                        });
                    }
                }
            }
        }

        private static void FindControllersClaims(List<Claims> claims, IEnumerable<Type> controllers)
        {
            foreach (Type controller in controllers)
            {
                var permissionAttributes = controller.GetCustomAttributes<Permission>(false);

                foreach (var attr in permissionAttributes)
                {
                    claims.Add(new Claims
                    {
                        ControllerName = controller.Name,
                        ClaimValue = attr.claimValue,
                        ClaimType = attr.claimType,
                    });
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ClaimsDetectorExtensions
    {
        public static IApplicationBuilder UseClaimsDetector(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsDetector>();
        }
    }
}
