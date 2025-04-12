using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Carter;

namespace Hackaton.Api.Controllers.Medicos
{
    public class MedicosController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/medicos")
                .RequireAuthorization("RouteAccessPolicy");

            grupo.MapPost("", Criar)
                .WithSummary("Grava um novo médico")
                .WithDescription("Grava um novo médico no banco de dados");

            grupo.MapPatch("{id}", AtualizarHorariosDisponiveis)
                .WithSummary("Atualiza horários do médico")
                .WithDescription("Atualiza os horários do médico no banco de dados");
        }

        private IResult Criar(MedicoDto dto, ICadastroAppService service)
        {
            service.CadastrarMedico(dto);

            return TypedResults.Created($"/{dto.Id}", dto);
        }

        private IResult AtualizarHorariosDisponiveis(int id, IList<HorarioDisponivelDto> dtoList, ICadastroAppService service)
        {
            service.CadastrarHorariosDisponiveis(id, dtoList);

            return TypedResults.Ok(dtoList);
        }
    }
}
