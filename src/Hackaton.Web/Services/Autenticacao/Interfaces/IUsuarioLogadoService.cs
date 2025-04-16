using System.Security.Claims;

namespace Hackaton.Web.Services.Autenticacao.Interfaces
{
    public interface IUsuarioLogadoService
    {
        Task<bool> IsAuthenticatedAsync();        
        Task<IDictionary<string, string>> ObterTodasAsClaimsAsync();
    }
}
