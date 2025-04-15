using Hackaton.Api.Controllers.Logins.Dtos;
using Hackaton.Api.Services.Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Hackaton.Api.Services.Jwt
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string GerarToken(TokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ObterArrayDeBytesdaChaveDaApi(configuration);

            var claims = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, request.Id),
                new Claim(ClaimTypes.Name, request.Nome),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, request.Perfil),
                new Claim("perfil", request.Perfil),
                new Claim("identificador", request.Identificador)
            ]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GerarRefreshToken(string usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ObterArrayDeBytesdaChaveDaApi(configuration);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, usuario));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidarRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ObterArrayDeBytesdaChaveDaApi(configuration);

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                    {
                        return notBefore <= DateTime.UtcNow && expires > DateTime.UtcNow;
                    },
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] ObterArrayDeBytesdaChaveDaApi(IConfiguration configuration)
        {
            var chave = configuration["Jwt:Key"];
            ArgumentException.ThrowIfNullOrEmpty(chave);
            return Encoding.ASCII.GetBytes(chave);
        }
    }
}
