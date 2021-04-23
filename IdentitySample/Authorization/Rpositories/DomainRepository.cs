using Authorization.Data;
using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class DomainRepository : Repository<Domain,Guid>, IDomainRepository
    {
        private readonly AuthorizationDbContext _context;
        public DomainRepository(AuthorizationDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
