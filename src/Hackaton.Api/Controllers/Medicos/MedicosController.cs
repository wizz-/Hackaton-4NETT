using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Application.Services.Medicos.Dtos;
using Application.Services.Medicos.Interfaces;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Medicos
{
    public class MedicosController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/medicos");

            grupo.MapPost("", Criar)
                .WithSummary("Grava um novo médico")
                .WithDescription("Grava um novo médico no banco de dados");

            grupo.MapPatch("{id}", AtualizarHorariosDisponiveis)
                .WithSummary("Atualiza horários do médico")
                .WithDescription("Atualiza os horários do médico no banco de dados")
                .RequireAuthorization();

            grupo.MapGet("{especialidadeId}", ObterMedicos)
                 .WithSummary("Obtém lista de médicos por especialidade")
                 .WithDescription("Obtém lista de médicos por especialidade no banco de dados")
                 .RequireAuthorization();
        }

        private IResult Criar(MedicoDto dto, ICadastroAppService service)
        {
            service.CadastrarMedico(dto);

            return TypedResults.Created($"/{dto.Id}");
        }

        private IResult AtualizarHorariosDisponiveis(int id, IList<HorarioDisponivelDto> dtoList, ICadastroAppService service)
        {
            service.CadastrarHorariosDisponiveis(id, dtoList);

            return TypedResults.Ok(dtoList);
        }

        private Ok<IList<MedicoParaConsultaDto>> ObterMedicos(int especialidadeId, IMedicoAppService service)
        {
            var retorno = service.ObterMedicosPorEspecialidade(especialidadeId);

            return TypedResults.Ok(retorno);
        }
    }
}
