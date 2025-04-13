using Hackaton.Web.Exceptions;
using Hackaton.Web.Models.Erros;
using Hackaton.Web.Models.Medico;
using System.Net.Http.Json;
using System.Text.Json;

namespace Hackaton.Web.Services.Medicos
{
    public class MedicoService(HttpClient http) : IMedicoService
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task CadastrarMedicoAsync(MedicoCadastroRequest medico)
        {
            var response = await http.PostAsJsonAsync("medicos", medico);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    var erro = JsonSerializer.Deserialize<ErroResponse>(content, _jsonSerializerOptions);

                    if (erro is not null)
                        throw new ApiException(erro.Mensagem, erro.Detalhes);
                }
                catch(JsonException)
                {
                    throw new ApiException("Erro inesperado ao processar resposta da API.");
                }
            }
        }
    }
}
