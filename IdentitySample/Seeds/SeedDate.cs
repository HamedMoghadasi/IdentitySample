using IdentitySample.Data;
using IdentitySample.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Utils
{
    public static class SeedExtension 
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ApplicationDbContext>();
                services.GetServices<ISeed>().ToList().ForEach(seeder => {
                    seeder.Seed(context);
                });
            }
            return host;
        }
    }
}
