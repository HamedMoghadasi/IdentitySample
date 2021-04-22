using Authorization.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Authorization.Filters
{
    public class RazorPermission : TypeFilterAttribute
        {
            private string _claimValue;
            private string _claimType = GlobalClaimsType.ViewPermission;

        public RazorPermission(string claimValue) : base(typeof(RazorPermissionFilter))
        {
            _claimValue = claimValue;
        } 

        public RazorPermission(string claimType, string claimValue) : base(typeof(RazorPermissionFilter))
            {
                _claimValue = claimValue;
                _claimType = claimType;
            }

            public string claimValue { get { return _claimValue; } private set { _claimValue = value; } }
            public string claimType { get { return _claimType; } private set { _claimType = value; } }
        }

        public class RazorPermissionFilter : IAuthorizationFilter
        {
            public RazorPermissionFilter() { }

            public void OnAuthorization(AuthorizationFilterContext context) { }
        }
    }

