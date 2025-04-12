using System.Security.Claims;

namespace Hackaton.Api.Services.Jwt.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(string usuario);
        string GerarRefreshToken(string usuario);
        ClaimsPrincipal? ValidarRefreshToken(string refreshToken);
    }
}
