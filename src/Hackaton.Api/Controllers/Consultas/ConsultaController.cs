using Application.Services.Calendarios.Interfaces;
using Application.Services.Consultas.Dtos;
using Carter;

namespace Hackaton.Api.Controllers.Consultas
{
    public class ConsultaController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var grupo = app.MapGroup("/api/consultas")
                .RequireAuthorization("RouteAccessPolicy");

            grupo.MapPost("", Criar)
                .WithSummary("Grava um nova consulta")
                .WithDescription("Grava um nova consulta no banco de dados");
        }

        private IResult Criar(ConsultaDto dto, ICalendarioAppService service)
        {
            throw new NotImplementedException();
            //service.(dto);

            //return TypedResults.Created($"/{dto.Id}", dto);
        }
    }
}
