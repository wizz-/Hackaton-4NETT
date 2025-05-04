using Application.Services.Consultas.Dtos;
using Application.Services.Consultas.Interfaces;
using Carter;
using Hackaton.Api.Controllers.Consultas.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Consultas
{
    public class ConsultaController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/consultas")
                .RequireAuthorization();

            grupo.MapPost("", Criar)
                .WithSummary("Grava uma nova consulta")
                .WithDescription("Grava uma nova consulta no banco de dados");

            grupo.MapGet("futuras/medicos/{medicoId}", ObterConsultasFuturasPorMedico)
                .WithSummary("Obtém consultas do médico")
                .WithDescription("Obtém consultas futuras do médico");

            grupo.MapGet("futuras/paciente/{pacienteId}", ObterConsultasFuturasPorPaciente)
                .WithSummary("Obtém consultas do paciente")
                .WithDescription("Obtém consultas futuras do paciente");

            grupo.MapPost("confirmar/{id}", Confirmar)
                .WithSummary("Confimar consulta")
                .WithDescription("Confirma uma consulta no banco de dados");

            grupo.MapPost("recusar/{id}", Recusar)
                .WithSummary("Recusa consulta")
                .WithDescription("Recusa consulta no banco de dados");

            grupo.MapPost("cancelar", Cancelar)
                .WithSummary("Cancelar consulta")
                .WithDescription("Cancela uma consulta no banco de dados");
        }

        private IResult Criar(ConsultaCadastroDto dto, IConsultaAppService service)
        {
            service.MarcarConsulta(dto);

            return TypedResults.Created($"/{dto.Id}", dto);
        }

        private IResult Confirmar(int id, IConsultaAppService service, HttpContext context)
        {
            var usuario = context.User.FindFirst("identificador")?.Value;
            if (usuario is null) TypedResults.BadRequest();

            service.ConfirmarConsulta(id, usuario[..6], usuario.Substring(6,2));

            return TypedResults.Ok();
        }

        private IResult Recusar(int id, IConsultaAppService service, HttpContext context)
        {
            var usuario = context.User.FindFirst("identificador")?.Value;
            if (usuario is null) TypedResults.BadRequest();

            service.RejeitarConsulta(id, usuario[..6], usuario.Substring(6, 2));

            return TypedResults.Ok();
        }

        private IResult Cancelar(CancelamentoDto cancelamento, IConsultaAppService service, HttpContext context)
        {
            var usuario = context.User.FindFirst("identificador")?.Value;

            service.CancelarConsulta(cancelamento.ConsultaId, usuario, cancelamento.Motivo);

            return TypedResults.Ok();
        }

        private Ok<IList<ConsultaDto>> ObterConsultasFuturasPorMedico(int medicoId, IConsultaAppService service)
        {
            var retorno = service.ObterConsultasDoMedico(medicoId);

            return TypedResults.Ok(retorno);
        }

        private Ok<IList<ConsultaDto>> ObterConsultasFuturasPorPaciente(int pacienteId, IConsultaAppService service)
        {
            var retorno = service.ObterConsultasDoPaciente(pacienteId);

            return TypedResults.Ok(retorno);
        }
    }
}
