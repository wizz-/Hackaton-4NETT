using Hackaton.Web.Models.Paciente;

namespace Hackaton.Web.Services.Agendas.Interfaces
{
    public interface IAgendaService
    {
        Task<IList<AgendaHorarioModel>> ObterAgendaDoMedico(DateOnly dataInicial, int medicoId, int quantidadeDeDias);
    }
}
