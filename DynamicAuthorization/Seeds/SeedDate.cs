using DynamicAuthorization.Seeds;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DynamicAuthorization.Utils
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
