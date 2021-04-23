using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> AddAsync(ApplicationUser entity)
        {
            return _userManager.CreateAsync(entity);
        }

        public IEnumerable<ApplicationUser> Find(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _userManager.Users.Where(predicate);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.AsEnumerable();
        }

        public Task<ApplicationUser> GetByIdAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public Task<ApplicationUser> GetByNameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<IdentityResult> Remove(ApplicationUser entity)
        {
            return _userManager.DeleteAsync(entity);
        }

        public ApplicationUser? SingleOrDefault(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _userManager.Users.SingleOrDefault(predicate);
        }

        public Task<IdentityResult> Update(ApplicationUser entity)
        {
            return _userManager.UpdateAsync(entity);
        }
    }
}
