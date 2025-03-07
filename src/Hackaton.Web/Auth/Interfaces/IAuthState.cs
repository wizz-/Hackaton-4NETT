using System.Security.Claims;

namespace Hackaton.Web.Auth.Interfaces
{
    public interface IAuthState
    {
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetUserNameAsync();
        Task<ClaimsPrincipal> GetUserAsync();
    }
}
