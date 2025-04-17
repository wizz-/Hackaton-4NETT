using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Application.Services.Medicos.Dtos;
using Application.Services.Medicos.Interfaces;
using Carter;
using Hackaton.Api.Controllers.Erros;
using Hackaton.Api.Controllers.Medicos.Dtos;
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
                .WithSummary("Atualiza dados e horários do médico")
                .WithDescription("Atualiza valor da consulta, especialidade e horários do médico no banco de dados")
                .RequireAuthorization();

            grupo.MapGet("por-especialidade/{especialidadeId}", ObterMedicos)
                 .WithSummary("Obtém lista de médicos por especialidade")
                 .WithDescription("Obtém lista de médicos por especialidade no banco de dados")
                 .RequireAuthorization();

            grupo.MapGet("{id}", ObterMedico)
                 .WithSummary("Obtém médico por id")
                 .WithDescription("Obtém médico por id do banco de dados")
                 .RequireAuthorization();
        }

        private Results<Created, BadRequest<ErroDto>> Criar(MedicoDto dto, ICadastroAppService service)
        {
            if (dto.Id > 0)
            {
                return TypedResults.BadRequest(new ErroDto() { Mensagem = "Argumento inválido." });
            }

            try
            {
                service.CadastrarMedico(dto);
                return TypedResults.Created($"/{dto.Id}");
            }
            catch (Exception erro)
            {
                return TypedResults.BadRequest(new ErroDto() { Mensagem = "Erro ao criar o Médico", Detalhes = erro.Message });
            }
        }

        private IResult AtualizarHorariosDisponiveis(int id, MedicoParaAtualizarDto dto, ICadastroAppService service)
        {
            service.CadastrarHorariosDisponiveis(id, dto.Especialidade, dto.ValorDaConsulta, dto.Horarios);

            return TypedResults.Ok();
        }

        private Ok<IList<MedicoParaConsultaDto>> ObterMedicos(int especialidadeId, IMedicoAppService service)
        {
            var retorno = service.ObterMedicosPorEspecialidade(especialidadeId);

            return TypedResults.Ok(retorno);
        }

        private Ok<CadastroMedicoDto> ObterMedico(int id, IMedicoAppService service)
        {
            var retorno = service.ObterDadosDoMedico(id);

            return TypedResults.Ok(retorno);
        }
    }
}
