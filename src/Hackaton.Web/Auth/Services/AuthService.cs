using Blazored.LocalStorage;
using Hackaton.Web.Auth.Blazor;
using Hackaton.Web.Auth.Interfaces;
using Hackaton.Web.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hackaton.Web.Auth.Services
{
    public class AuthService(CustomAuthStateProvider _authStateProvider) : IAuthService
    {
        public async Task<bool> LoginAsync(LoginModel login)
        {
            if (
                (login.Perfil == PerfisDeUsuario.Medico && login.CRM == "123" && login.Senha == "senha") 
                || (login.Perfil == PerfisDeUsuario.Paciente && login.EmailOuCPF == "paciente@email.com" && login.Senha == "senha"))
            {
                var token = GerarJwtMockado("Thiago Rosa");
                await _authStateProvider.MarkUserAsAuthenticated(token);
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _authStateProvider.MarkUserAsLoggedOut();
        }

        private static string GerarJwtMockado(string username)
        {
            var chaveSecreta = "chave-super-secreta-para-mock-123456";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken(
                issuer: "Hackaton.Mock",
                audience: "Hackaton.Web",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
