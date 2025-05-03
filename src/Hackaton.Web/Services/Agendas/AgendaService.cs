using Hackaton.Web.Exceptions;
using Hackaton.Web.Infra;
using Hackaton.Web.Models.Erros;
using Hackaton.Web.Models.Paciente;
using Hackaton.Web.Services.Agendas.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Hackaton.Web.Services.Agendas
{
    public class AgendaService(HttpClient http) : IAgendaService
    {
        public async Task<IList<AgendaHorarioModel>> ObterAgendaDoMedico(DateOnly dataInicial, int medicoId, int quantidadeDeDias)
        {
            var requestBody = new AgendaRequestModel()
            {
                DataInicial = dataInicial,
                MedicoId = medicoId,
                QuantidadeDeDias = quantidadeDeDias,
            };

            var response = await http.PostAsJsonAsync("agenda/medico", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    var erro = JsonSerializer.Deserialize<ErroResponse>(content, JsonOptionsDefaults.Web);

                    if (erro is not null)
                        throw new ApiException(erro.Mensagem, erro.Detalhes);
                }
                catch (JsonException)
                {
                    throw new ApiException("Erro inesperado ao processar resposta da API.");
                }
            }

            var resultado = await response.Content.ReadFromJsonAsync<IList<AgendaHorarioModel>>();

            return resultado!;

        }
    }
}
