using System.Collections.Generic;

namespace IdentitySample.DTOs.Users
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
