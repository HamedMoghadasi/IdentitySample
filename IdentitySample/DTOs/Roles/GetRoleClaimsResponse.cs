using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.DTOs.Roles
{
    public class GetRoleClaimsResponse
    {
        public Claims Claims{ get; set; }
        public bool Selected { get; set; }
    }
}
