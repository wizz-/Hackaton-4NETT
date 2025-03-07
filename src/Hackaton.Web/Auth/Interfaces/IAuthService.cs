using Hackaton.Web.Models;

namespace Hackaton.Web.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel login);
        Task LogoutAsync();
    }
}
