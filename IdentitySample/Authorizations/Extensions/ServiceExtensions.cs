using Authorization.Filters.RazorSecurity;
using Authorization.Seeds;
using Authorization.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Authorizations.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPermissionMiddleware(this IServiceCollection services) {

            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddScoped<ISecurity, RazorSecurity>();
            services.AddScoped<ISeed, ClaimsSeed>();
            services.AddScoped<ISeed, RolesSeed>();
            services.AddScoped<ISeed, UsersSeed>();
            services.AddScoped<ISeed, RoleClaimsSeed>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/identity/Account/Login");
                options.AccessDeniedPath = new PathString("/identity/Account/AccessDenied");
            });
        }
    }
}
