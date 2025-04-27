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

            grupo.MapPost("especialidade", ObterDisponibilidadePorEspecialidade)
                .WithSummary("Obtém horários disponíveis")
                .WithDescription("Retorna os horários disponiveis por especialidade cadastradas no banco de dados");

            grupo.MapPost("medico", ObterDisponibilidadePorMedico)
                .WithSummary("Obtém horários disponíveis")
                .WithDescription("Retorna os horários disponiveis por médico cadastradas no banco de dados");
        }

        private Ok<IList<MedicoDisponivelDto>> ObterDisponibilidadePorEspecialidade(ParametroPorEspecialidadeDto parametros, IAgendaAppService service)
        {
            var retorno = service.ObterAgendaPorEspecialidade(parametros.Dia, parametros.EspecialidadeId);

            return TypedResults.Ok(retorno);
        }

        private Ok<IList<AgendaAppDto>> ObterDisponibilidadePorMedico(ParametroPorMedicoDto parametros, IAgendaAppService service)
        {
            var retorno = service.ObterAgendaPorMedico(parametros.DataInicial, parametros.QuantidadeDeDias, parametros.MedicoId);

            return TypedResults.Ok(retorno);
        }
    }
}
