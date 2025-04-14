using Hackaton.Web.Services.Autenticacao.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hackaton.Web.Services.Autenticacao
{
    public class ConsultaAutenticacaoService(AuthenticationStateProvider _authProvider) : IConsultaAutenticacaoService
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }

        public async Task<string?> GetUserNameAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.Name;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User;
        }
    }
}
