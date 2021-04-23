using Authorization.Data;
using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class RoleClaimRepository : Repository<RoleClaim,Guid>, IRoleClaimRepository
    {
        private readonly AuthorizationDbContext _context;
        public RoleClaimRepository(AuthorizationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Claims> GetClaims(ApplicationRole Role)
        {
            return GetAll().Where(i => i.RoleId == Role.Id).Select(i => i.Claims);
        }
    }
}
