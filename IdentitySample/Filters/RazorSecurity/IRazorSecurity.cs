using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Filters.RazorSecurity
{
    public interface IRazorSecurity
    {
        bool IsGranted(string claimValue);
        bool IsGranted(string claimType , string claimValue);
    }
}
