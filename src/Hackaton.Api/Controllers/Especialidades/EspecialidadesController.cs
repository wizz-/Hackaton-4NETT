using Application.Services.Cadastros.Dtos;
using Application.Services.Especialidades.Interfaces;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Especialidades
{
    public class EspecialidadesController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/especialidades")
                .RequireAuthorization();

            grupo.MapGet("", ObterEspecialidades)
                .WithSummary("Obtém especialidades")
                .WithDescription("Retorna todas as especialidades cadastradas no banco de dados");
        }

        private Ok<IList<EspecialidadeDto>> ObterEspecialidades(IEspecialidadesAppService service)
        {
            var retorno = service.ObterEspecialidades();

            return TypedResults.Ok(retorno);
        }
    }
}
