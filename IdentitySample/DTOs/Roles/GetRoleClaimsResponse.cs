using Authorization.Models;

namespace IdentitySample.DTOs.Roles
{
    public class GetRoleClaimsResponse
    {
        public Claims Claims{ get; set; }
        public bool Selected { get; set; }
    }
}
