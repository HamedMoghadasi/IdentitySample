using Authorization.Filters;
using Authorization.Models;
using Authorization.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Authorization.Data;
using Authorization.Rpositories;

namespace Authorization.Seeds
{
    public class ClaimsSeed : ISeed
    {
        private readonly IClaimsRepository _claimsRepository;
        public ClaimsSeed(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }
        public double ExecutionOrder => 4;
        public void Seed()
        {
            var claims = new List<Claims>();

            var controllers = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(i => typeof(ControllerBase).IsAssignableFrom(i))
                .Select(i => i);

            FindClaims(claims, controllers);
            FindRazorsClaims(claims, controllers);
            var distinctClaims = claims.Distinct(new ClaimsComparer()).ToList();

            InsertClaims(distinctClaims);
        }

        private void InsertClaims(List<Claims> claimFilters)
        {
            var dbClaims = _claimsRepository.GetAll();
            var removedClaimsFilters = dbClaims.Except(claimFilters, new ClaimsComparer()).ToList();
            foreach (var item in claimFilters)
            {
                if (!IsExisted(dbClaims, item))
                {
                    _claimsRepository.Add(item);
                }
                else if (IsModified(dbClaims, item)) 
                {
                    var targetClaim = GetModified(dbClaims,item);
                    _claimsRepository.Update(targetClaim);
                }
            }
            foreach (var item in removedClaimsFilters)
            {
                if (IsExisted(dbClaims, item))
                {
                    _claimsRepository.Remove(item);

                }
            }
            _claimsRepository.SaveChanges();
        }

        private Claims GetModified(IEnumerable<Claims> dbClaims, Claims item)
        {
            var targetClaim = dbClaims.FirstOrDefault(i => i.ControllerName == item.ControllerName && i.ActionName == item.ActionName && i.ClaimType == item.ClaimType && i.ClaimValue == item.ClaimValue);
            targetClaim.DisplayName = item.DisplayName;
            return targetClaim;
        }

        private bool IsModified(IEnumerable<Claims> dbClaims, Claims item)
        {
            return dbClaims.Any(i => i.ControllerName == item.ControllerName && i.ActionName == item.ActionName && i.ClaimType == item.ClaimType && i.ClaimValue == item.ClaimValue && i.DisplayName != item.DisplayName);
        }

        private bool IsExisted(IEnumerable<Claims> dbClaims, Claims item)
        {
            return dbClaims.Any(i => i.ControllerName == item.ControllerName && i.ActionName == item.ActionName && i.ClaimType == item.ClaimType && i.ClaimValue == item.ClaimValue);
        }

        private void FindClaims(List<Claims> claims, IEnumerable<Type> controllers)
        {
            foreach (TypeInfo controller in controllers)
            {
                //FindContollerClaims(claims, controller);
                FindActionsClaims(claims, controller);
            }
        }

        private static void FindActionsClaims(List<Claims> claims, TypeInfo controller)
        {
            var actions = controller.DeclaredMethods;
            foreach (var action in actions)
            {
                if (IsProtectedAction(controller, action))
                {
                    var permissionDisplayNameAttribute = action.GetCustomAttributes<PermissionDisplayName>(false).FirstOrDefault();
                    claims.Add(new Claims
                    {
                        ActionName = action.Name,
                        ControllerName = controller.Name,
                        ClaimValue = $"{controller.Name}.{action.Name}",
                        ClaimType = GlobalClaimsType.Permission,
                        DisplayName = permissionDisplayNameAttribute != null
                        ? permissionDisplayNameAttribute.DisplayName
                        : $"Access {action.Name} in {controller.Name} "
                    });
                }
            }
        }

        private void FindRazorsClaims(List<Claims> claims, IEnumerable<Type> controllers)
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
                            DisplayName = $"{action.DeclaringType.Name}.{action.Name}.{attr.claimValue}"
                        });
                    }
                }
            }
        }

        private static bool IsProtectedAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            if (actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
                return false;

            if (controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                return true;

            if (actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                return true;

            return false;
        }
    }
}

