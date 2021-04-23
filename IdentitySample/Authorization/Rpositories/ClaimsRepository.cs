using Authorization.Data;
using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class ClaimsRepository: Repository<Claims,Guid>, IClaimsRepository
    {
        private readonly AuthorizationDbContext _context;
        public ClaimsRepository(AuthorizationDbContext context)
            : base(context)
        {
            _context = context;        
        }
    }
}
