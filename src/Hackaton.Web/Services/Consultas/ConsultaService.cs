using Hackaton.Web.Models.Paciente;
using Hackaton.Web.Services.Consultas.Interfaces;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Consultas
{
    public class ConsultaService(HttpClient http) : IConsultaService
    {
        public async Task<IList<ConsultaPacienteModel>> ObterConsultasFuturasDoPaciente(int pacienteId)
        {
            var response = await http.GetFromJsonAsync<IList<ConsultaPacienteModel>>($"consultas/futuras/paciente/{pacienteId}");

            return response ?? [];

        }
    }
}
