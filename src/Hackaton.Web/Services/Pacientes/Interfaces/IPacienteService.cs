using Hackaton.Web.Models.Paciente;

namespace Hackaton.Web.Services.Pacientes.Interfaces
{
    public interface IPacienteService
    {
        Task CadastrarPacienteAsync(PacienteCadastroRequest paciente);
    }
}
