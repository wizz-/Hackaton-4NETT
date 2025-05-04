using Hackaton.Web.Models.Medico;
using Hackaton.Web.Models.Paciente;

namespace Hackaton.Web.Services.Consultas.Interfaces
{
    public interface IConsultaService
    {
        Task<IList<ConsultaPacienteModel>> ObterConsultasFuturasDoPaciente(int pacienteId);
        Task<IList<ConsultaMedicoModel>> ObterConsultasFuturasDoMedicoAsync(int medicoId);
        Task CancelarConsulta(int consultaId, string motivo);
        Task CriarConsulta(CriarConsultaRequest consultaRequest);
        Task ConfirmarConsultaAsync(int consultaId);
        Task RejeitarConsultaAsync(int consultaId);
    }
}
