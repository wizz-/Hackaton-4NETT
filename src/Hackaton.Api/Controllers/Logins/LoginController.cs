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
            var paciente = service.LoginPaciente(dto.cpfEmail, dto.Senha);

            var request = new TokenRequest
            {
                Id = paciente!.Id.ToString(),
                Nome = paciente!.Nome!,
                Email = paciente!.Email!,
                Perfil = "Paciente",
                Identificador = paciente!.Cpf!
            };

            var jwts = new LoginResponseDto()
            {
                Token = tokenService.GerarToken(request),
                RefreshToken = tokenService.GerarRefreshToken(request.Identificador)
            };

            return TypedResults.Ok(jwts);
        }

        private IResult LoginMedico(MedicoLoginDto dto, ILoginAppService service, ITokenService tokenService)
        {
            var medico = service.LoginMedico(dto.Crm, dto.Uf, dto.Senha); // deve retornar um objeto com ID, nome e email

            var request = new TokenRequest
            {
                Id = medico.Id.ToString(),
                Nome = medico.Nome,
                Email = "",
                Perfil = "Medico",
                Identificador = $"{dto.Crm}{dto.Uf}"
            };

            var jwts = new LoginResponseDto
            {
                Token = tokenService.GerarToken(request),
                RefreshToken = tokenService.GerarRefreshToken(request.Identificador)
            };

            return TypedResults.Ok(jwts);
        }
    }
}
