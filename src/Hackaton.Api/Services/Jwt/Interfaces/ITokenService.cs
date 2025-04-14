using Hackaton.Api.Controllers.Logins.Dtos;
using System.Security.Claims;

namespace Hackaton.Api.Services.Jwt.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(TokenRequest request);
        string GerarRefreshToken(string usuario);
        ClaimsPrincipal? ValidarRefreshToken(string refreshToken);
    }
}
