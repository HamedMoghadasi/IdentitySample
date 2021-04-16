using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
