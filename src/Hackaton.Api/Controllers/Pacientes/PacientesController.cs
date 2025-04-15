using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Carter;
using Hackaton.Api.Controllers.Erros;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Pacientes
{
    public class PacientesController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/pacientes");

            grupo.MapPost("", Criar)
                .WithSummary("Grava um novo paciente")
                .WithDescription("Grava um novo paciente no banco de dados");
        }

        private Results<Created, BadRequest<ErroDto>> Criar(PacienteDto dto, ICadastroAppService service)
        {
            if (dto.Id > 0)
            {
                return TypedResults.BadRequest(new ErroDto() { Mensagem = "Argumento inválido." });
            }

            try
            {
                service.CadastrarPaciente(dto);
                return TypedResults.Created($"/{dto.Id}");
            } 
            catch(Exception erro)
            {
                return TypedResults.BadRequest(new ErroDto() { Mensagem = "Erro ao criar o Paciente", Detalhes = erro.Message });
            }
        }
    }
}
