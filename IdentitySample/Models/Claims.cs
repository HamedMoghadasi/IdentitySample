using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    public class Claims
    {
        public int Id { get; set; }
        public string ControllerName { get; set; } 
        public string ActionName { get; set; } 
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string  DisplayName { get; set; }
    }

    public class ClaimsComparer : IEqualityComparer<Claims>
    {
        public bool Equals(Claims x, Claims y)
        {
            return x.ClaimType == y.ClaimType && x.ClaimValue == y.ClaimValue;
        }

        public int GetHashCode([DisallowNull] Claims obj)
        {
            return obj.ClaimType.GetHashCode() & obj.ClaimValue.GetHashCode();
        }
    }
}
