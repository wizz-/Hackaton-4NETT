using Application.Services.Agendas.Dtos;

namespace Application.Services.Agendas.Interfaces
{
    public interface IAgendaAppService
    {
        IList<MedicoDisponivelDto> ObterAgenda(DateOnly dia, int especialidadeId);
    }
}
