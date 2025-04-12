using Application.Services.Agendas.Dtos;
using Application.Services.Agendas.Interfaces;
using Carter;
using Hackaton.Api.Controllers.Agendas.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Agendas
{
    public class AgendaController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/agenda")
                .RequireAuthorization();

            grupo.MapPost("", ObterDisponibilidade)
                .WithSummary("Obtém horários disponíveis")
                .WithDescription("Retorna os horários disponiveis cadastradas no banco de dados");
        }

        private Ok<IList<MedicoDisponivelDto>> ObterDisponibilidade(ParametroDto parametros, IAgendaAppService service)
        {
            var retorno = service.ObterAgenda(parametros.Dia, parametros.EspecialidadeId);

            return TypedResults.Ok(retorno);
        }
    }
}
