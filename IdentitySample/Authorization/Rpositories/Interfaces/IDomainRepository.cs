using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public interface IDomainRepository: IRepository<Domain, Guid>
    {
    }
}
