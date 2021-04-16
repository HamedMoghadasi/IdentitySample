using IdentitySample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Seeds
{
    public class UsersSeed : ISeed
    {
        public void Seed(ApplicationDbContext context)
        {
            Console.WriteLine("user seed");
        }
    }
}
