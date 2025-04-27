using Hackaton.Web.Exceptions;
using Hackaton.Web.Models.Medico;
using Hackaton.Web.Services.Agendas.Interfaces;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Agendas
{
    public class AgendaService(HttpClient http) : IAgendaService
    {
        public async Task<IList<MedicoParaConsultaModel>> ObterTodosMedicos()
        {
            try
            {
                var medicos = await http.GetFromJsonAsync<IList<MedicoParaConsultaModel>>($"medicos");

                return medicos!;

            }
            catch (Exception ex)
            {
                throw new ApiException("Erro inesperado ao processar resposta da API.");
            }
        }
    }
}
