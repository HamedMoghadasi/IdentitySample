using Microsoft.AspNetCore.Identity;
using System;

namespace DynamicAuthorization.Models
{
    public class ApplicationRoleClaim: IdentityRoleClaim<Guid>
    {
    }
}
