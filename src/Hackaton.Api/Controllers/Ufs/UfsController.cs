using Application.Services.Ufs.Interfaces;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hackaton.Api.Controllers.Ufs
{
    public class UfsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/ufs")
                .RequireAuthorization();

            grupo.MapGet("", ObterUfs)
                .WithSummary("Obtém UFs")
                .WithDescription("Retorna todas as UF do enum UnidadeFederativa");
        }

        private Ok<IList<string>> ObterUfs(IUfAppService service)
        {
            var retorno = service.ObterUfs();

            return TypedResults.Ok(retorno);
        }
    }
}
