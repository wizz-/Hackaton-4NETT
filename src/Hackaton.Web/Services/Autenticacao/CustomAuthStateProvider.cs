using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace Hackaton.Web.Services.Autenticacao
{
    public class CustomAuthStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        private const string TokenKey = "authToken";
        private ClaimsPrincipal _usuarioAtual = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsStringAsync(TokenKey);

            if (string.IsNullOrWhiteSpace(token))
                return CriarEstadoNaoAutenticado();

            var jwt = LerJwt(token);
            if (jwt is null || TokenEstaExpirado(jwt))
            {
                await RemoverTokenAsync();
                return CriarEstadoNaoAutenticado();
            }

            _usuarioAtual = CriarUsuarioAutenticado(jwt);
            return new AuthenticationState(_usuarioAtual);
        }

        public async Task MarcarComoAutenticadoAsync(string token)
        {
            await localStorage.SetItemAsStringAsync(TokenKey, token);
            _usuarioAtual = CriarUsuarioAutenticado(LerJwt(token));
            NotificarAlteracaoEstado();
        }

        public async Task MarcarComoDeslogadoAsync()
        {
            await RemoverTokenAsync();
            _usuarioAtual = new ClaimsPrincipal(new ClaimsIdentity());
            NotificarAlteracaoEstado();
        }

        private static JwtSecurityToken? LerJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }

        private static bool TokenEstaExpirado(JwtSecurityToken jwt)
        {
            var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expClaim is null || !long.TryParse(expClaim, out var exp))
                return true;

            return DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime < DateTime.UtcNow;
        }

        private static ClaimsPrincipal CriarUsuarioAutenticado(JwtSecurityToken? jwt)
        {
            if (jwt is null)
                return new ClaimsPrincipal(new ClaimsIdentity());

            return new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt"));
        }

        private AuthenticationState CriarEstadoNaoAutenticado()
            => new(new ClaimsPrincipal(new ClaimsIdentity()));

        private void NotificarAlteracaoEstado()
            => NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_usuarioAtual)));

        private async Task RemoverTokenAsync()
            => await localStorage.RemoveItemAsync(TokenKey);
    }
}
