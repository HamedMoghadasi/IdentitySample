using Authorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class Domain : ModelBase
    {
        public string Name { get; set; }

        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }
}
