using DynamicAuthorization.Core;
using DynamicAuthorization.Data;
using DynamicAuthorization.Filters.Security;
using DynamicAuthorization.Seeds;
using DynamicAuthorization.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentitySample.Authorizations.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPermissionMiddleware<TDbContext>(this IServiceCollection services) 
            where TDbContext: DbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ISecurity, Security>();
            services.AddScoped<ISeed, ClaimsSeed>();
            services.AddScoped<ISeed, RolesSeed>();
            services.AddScoped<ISeed, UsersSeed>();
            services.AddScoped<ISeed, RoleClaimsSeed>();
            services.AddTransient<IAuthorizationDbContext, AuthorizationDbContext<TDbContext>>();
        }
    }
}
