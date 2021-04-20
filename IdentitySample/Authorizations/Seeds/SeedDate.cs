using IdentitySample.Data;
using Authorization.Seeds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Authorization.Utils
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
                    seeder.Seed();
                });
            }
            return host;
        }
    }
}
