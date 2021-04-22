using System.Threading.Tasks;

namespace Authorization.Filters.Security
{
    public interface ISecurity
    {
        Task<bool> IsGrantedAsync(string claimType , string claimValue);
        Task<bool> IsGrantedAsync(string roleName);
    }
}
