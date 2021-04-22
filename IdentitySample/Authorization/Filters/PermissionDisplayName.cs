using Microsoft.AspNetCore.Mvc.Filters;

namespace Authorization.Filters
{
    public class PermissionDisplayName : ActionFilterAttribute
    {
        public PermissionDisplayName(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
