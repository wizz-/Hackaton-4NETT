using Application.Services.Agendas.Dtos;

namespace Application.Services.Agendas.Interfaces
{
    public interface IAgendaAppService
    {
        IList<MedicoDisponivelDto> ObterAgendaPorEspecialidade(DateOnly dia, int especialidadeId);
        IList<AgendaAppDto> ObterAgendaPorMedico(DateOnly diaInicial, int quantidadeDeDias, int medicoId);

    }
}
