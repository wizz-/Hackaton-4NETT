using Hackaton.Web.Models;
using Hackaton.Web.Models.Autenticacao;

namespace Hackaton.Web.Services.Autenticacao.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<bool> LoginAsync(AutenticacaoModel login);
        Task LogoutAsync();
    }
}
