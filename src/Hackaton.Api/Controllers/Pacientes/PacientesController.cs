using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Carter;

namespace Hackaton.Api.Controllers.Pacientes
{
    public class PacientesController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/pacientes")
                .RequireAuthorization("RouteAccessPolicy");

            grupo.MapPost("", Criar)
                .WithSummary("Grava um novo paciente")
                .WithDescription("Grava um novo paciente no banco de dados");
        }

        private IResult Criar(PacienteDto dto, ICadastroAppService service)
        {
            service.CadastrarPaciente(dto);

            return TypedResults.Created($"/{dto.Id}", dto);
        }
    }
}
