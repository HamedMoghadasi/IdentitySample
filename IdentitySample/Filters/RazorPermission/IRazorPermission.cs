using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Filters.RazorPermission
{
    public interface IRazorPermission
    {
        bool IsGranted(string claimType , string claimValue);
    }
}
