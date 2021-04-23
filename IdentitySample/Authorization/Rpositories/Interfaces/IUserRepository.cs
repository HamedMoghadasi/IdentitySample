using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddAsync(ApplicationUser entity);
        IEnumerable<ApplicationUser> Find(Expression<Func<ApplicationUser, bool>> predicate);
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IdentityResult> Remove(ApplicationUser entity);
        ApplicationUser? SingleOrDefault(Expression<Func<ApplicationUser, bool>> predicate);
        Task<IdentityResult> Update(ApplicationUser entity);
        Task<ApplicationUser> GetByNameAsync(string username);
    }
}
