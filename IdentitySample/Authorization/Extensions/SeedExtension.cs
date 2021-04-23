using Authorization.Seeds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Authorization.Extensions
{
    public static class SeedExtension 
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var Seeds = services.GetServices<ISeed>().OrderBy(i => i.ExecutionOrder).ToList();
                Seeds.ForEach(seeder => {
                    seeder.Seed();
                });
            }
            return host;
        }
    }
}
