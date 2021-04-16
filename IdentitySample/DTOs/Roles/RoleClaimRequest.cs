using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.DTOs.Roles
{
    public class RoleClaimRequest
    {
        public string RoleName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
