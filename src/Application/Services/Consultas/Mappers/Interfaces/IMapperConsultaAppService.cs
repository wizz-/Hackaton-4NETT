using Application.Services.Consultas.Dtos;
using Domain.Entities.Consultas;

namespace Application.Services.Consultas.Mappers.Interfaces
{
    public interface IMapperConsultaAppService
    {
        IList<ConsultaDto> Map(IList<Consulta> consultas);
    }
}
