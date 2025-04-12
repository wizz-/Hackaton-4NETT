using Application.Services.Logins.Interfaces;
using Carter;
using Hackaton.Api.Controllers.Logins.Dtos;
using Hackaton.Api.Services.Jwt.Interfaces;

namespace Hackaton.Api.Controllers.Logins
{
    public class LoginController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/login");

            grupo.MapPost("paciente", LoginPaciente)
                .WithSummary("Efetua login do Paciente")
                .WithDescription("Efetua login de um paciente");


            grupo.MapPost("medico", LoginMedico)
                .WithSummary("Efetua login do médico")
                .WithDescription("Efetua login de um médico");
        }

        private IResult LoginPaciente(PacienteLoginDto dto, ILoginAppService service, ITokenService tokenService)
        {
            service.LoginPaciente(dto.cpfEmail, dto.Senha);

            var jwts = new LoginResponseDto()
            {
                Token = tokenService.GerarToken(dto.cpfEmail),
                RefreshToken = tokenService.GerarRefreshToken(dto.cpfEmail)
            };

            return TypedResults.Ok(jwts);
        }

        private IResult LoginMedico(MedicoLoginDto dto, ILoginAppService service, ITokenService tokenService)
        {
            service.LoginMedico(dto.Crm, dto.Uf, dto.Senha);

            var jwts = new LoginResponseDto()
            {
                Token = tokenService.GerarToken($"{dto.Crm}{dto.Uf}"),
                RefreshToken = tokenService.GerarRefreshToken($"{dto.Crm}{dto.Uf}")
            };

            return TypedResults.Ok(jwts);
        }
    }
}
