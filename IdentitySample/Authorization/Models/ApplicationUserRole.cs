using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class ApplicationUserRole: IdentityUserRole<Guid>
    {
    }
}
