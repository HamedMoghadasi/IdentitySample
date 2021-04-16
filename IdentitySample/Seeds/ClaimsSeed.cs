using IdentitySample.Data;
using IdentitySample.Filters;
using IdentitySample.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentitySample.Seeds
{
        public class ClaimsSeed: ISeed
        {
            public void Seed(ApplicationDbContext context)
            {
                var claims = new List<Claims>();

                var controllers = Assembly.GetExecutingAssembly()
                    .GetExportedTypes()
                    .Where(i => typeof(ControllerBase).IsAssignableFrom(i))
                    .Select(i => i);

                FindControllersClaims(claims, controllers);
                FindActionsClaims(claims, controllers);
                FindRazorsClaims(claims, controllers);


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

            private static void FindRazorsClaims(List<Claims> claims, IEnumerable<Type> controllers)
            {
                foreach (Type controller in controllers)
                {
                    var actions = controller.GetMethods().Where(i => i.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && i.CustomAttributes.Count() > 0).ToList();
                    foreach (var action in actions)
                    {
                        var permissionAttributes = action.GetCustomAttributes<RazorPermission>(false);
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
        }
    }

