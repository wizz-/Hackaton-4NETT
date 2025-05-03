using Hackaton.Web.Models.Paciente;

namespace Hackaton.Web.Services.Consultas.Interfaces
{
    public interface IConsultaService
    {
        Task<IList<ConsultaPacienteModel>> ObterConsultasFuturasDoPaciente(int pacienteId);
        Task CancelarConsulta(int consultaId, string motivo);
    }
}
