using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.DTOs.Users
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
