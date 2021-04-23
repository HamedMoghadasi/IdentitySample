using Authorization.Data;
using Authorization.Filters.Security;
using Authorization.Models;
using Authorization.Rpositories;
using Authorization.Seeds;
using Authorization.Stores;
using Authorization.Utils;
using IdentitySample.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorizations.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPermissionMiddleware<TContext>(this IServiceCollection services) 
            where TContext : DbContext
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddUserStore<ApplicationUserStore>()
               .AddUserManager<ApplicationUserManager>()
               .AddRoleStore<ApplicationRoleStore>()
               .AddRoleManager<ApplicationRoleManager>()
               .AddDefaultTokenProviders();

            services.AddRazorPages();
            services.AddHttpContextAccessor();

            services.AddScoped<ISecurity, Security>();

            services.AddScoped<ISeed, ClaimsSeed>();
            services.AddScoped<ISeed, DomainSeed>();
            services.AddScoped<ISeed, RolesSeed>();
            services.AddScoped<ISeed, UsersSeed>();
            services.AddScoped<ISeed, RoleClaimsSeed>();
            
            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<IDomainRepository, DomainRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient(typeof(AuthorizationDbContext), typeof(TContext));


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/identity/Account/Login");
                options.AccessDeniedPath = new PathString("/identity/Account/AccessDenied");
            });

            //services.AddScoped<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>, ApplicationUserStore>();
            //services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
            //services.AddScoped<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            //services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            //services.AddScoped<RoleStore<ApplicationRole, ApplicationDbContext, Guid, ApplicationUserRole, ApplicationRoleClaim>, ApplicationRoleStore>();
        }
    }
}
