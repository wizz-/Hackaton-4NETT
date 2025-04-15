using Hackaton.Web.Exceptions;
using Hackaton.Web.Infra;
using Hackaton.Web.Models.Erros;
using Hackaton.Web.Models.Paciente;
using Hackaton.Web.Models.Usuario;
using Hackaton.Web.Services.Pacientes.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Hackaton.Web.Services.Pacientes
{
    public class PacienteService(HttpClient http) : IPacienteService
    {
        public async Task CadastrarPacienteAsync(PacienteModel paciente)
        {
            var parametroDaRequest = PreencherPacienteRequest(paciente);
            var response = await http.PostAsJsonAsync("pacientes", parametroDaRequest);

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
        }

        private static PacienteCadastroRequest PreencherPacienteRequest(PacienteModel model)
        {
            return new PacienteCadastroRequest
            {
                Nome = model.NomeCompleto,
                CPF = model.CPF,
                Email = model.Email,
                Usuario = new UsuarioRequest
                {
                    Email = model.Email,
                    Senha = model.Senha
                }
            };
        }
    }
}
