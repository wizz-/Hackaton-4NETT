using Application.Services.Medicos.Dtos;
using Domain.Entities.Cadastros;

namespace Application.Services.Medicos.Mappers.Interfaces
{
    public interface IMapperMedicoAppService
    {
        CadastroMedicoDto Map(Medico medico);
        IList<MedicoParaConsultaDto> Map(IList<Medico> medicos);
    }
}
