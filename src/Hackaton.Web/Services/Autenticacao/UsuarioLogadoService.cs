using Hackaton.Web.Services.Autenticacao.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hackaton.Web.Services.Autenticacao
{
    public class UsuarioLogadoService(AuthenticationStateProvider _authProvider) : IUsuarioLogadoService
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }       

        public async Task<IDictionary<string, string>> ObterTodasAsClaimsAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.Claims.ToDictionary(c => c.Type, c => c.Value);
        }
    }
}
