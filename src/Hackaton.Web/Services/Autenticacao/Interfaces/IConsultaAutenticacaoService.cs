using System.Security.Claims;

namespace Hackaton.Web.Services.Autenticacao.Interfaces
{
    public interface IConsultaAutenticacaoService
    {
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetUserNameAsync();
        Task<ClaimsPrincipal> GetUserAsync();
    }
}
