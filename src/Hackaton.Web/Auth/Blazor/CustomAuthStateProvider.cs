using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace Hackaton.Web.Auth.Blazor
{
    public class CustomAuthStateProvider(ILocalStorageService _localStorage) : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(_currentUser);
            }

            var jwt = ParseJwt(token);

            if (jwt == null || TokenExpirado(jwt))
            {
                await MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _currentUser = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt"));
            return new AuthenticationState(_currentUser);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsStringAsync("authToken", token);

            var jwt = ParseJwt(token);
            if (jwt != null)
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt"));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");

            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        private static JwtSecurityToken? ParseJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }

        private static bool TokenExpirado(JwtSecurityToken jwt)
        {
            var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (expClaim is null || !long.TryParse(expClaim, out var expTimestamp))
                return true; // Assume como expirado se não houver claim válida.

            var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;
            return expDateTime < DateTime.UtcNow; // Retorna `true` se o token já expirou.
        }
    }
}
