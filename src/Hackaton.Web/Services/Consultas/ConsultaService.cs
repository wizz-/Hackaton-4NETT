﻿using Hackaton.Web.Exceptions;
using Hackaton.Web.Infra;
using Hackaton.Web.Models.Erros;
using Hackaton.Web.Models.Medico;
using Hackaton.Web.Models.Paciente;
using Hackaton.Web.Services.Consultas.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Hackaton.Web.Services.Consultas
{
    public class ConsultaService(HttpClient http) : IConsultaService
    {
        public async Task<IList<ConsultaPacienteModel>> ObterConsultasFuturasDoPaciente(int pacienteId)
        {
            var response = await http.GetFromJsonAsync<IList<ConsultaPacienteModel>>($"consultas/futuras/paciente/{pacienteId}");

            return response ?? [];
        }

        public async Task<IList<ConsultaMedicoModel>> ObterConsultasFuturasDoMedicoAsync(int medicoId)
        {
            var response = await http.GetFromJsonAsync<IList<ConsultaMedicoModel>>($"consultas/futuras/medicos/{medicoId}");

            return response ?? [];
        }

        public async Task CriarConsulta(CriarConsultaRequest consultaRequest)
        {
            var response = await http.PostAsJsonAsync("consultas", consultaRequest);

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

        public async Task CancelarConsulta(int consultaId, string motivo)
        {
            var parametroDaRequest = PreencherCancelarConsultaRequest(consultaId, motivo);

            var response = await http.PostAsJsonAsync("consultas/cancelar", parametroDaRequest);

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

        private CancelarConsultaRequest PreencherCancelarConsultaRequest(int consultaId, string motivo)
        {
            return new CancelarConsultaRequest()
            {
                ConsultaId = consultaId,
                Motivo = motivo
            };
        }

        public async Task ConfirmarConsultaAsync(int consultaId)
        {
            var response = await http.PostAsync($"consultas/confirmar/{consultaId}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task RejeitarConsultaAsync(int consultaId)
        {
            var response = await http.PostAsync($"consultas/recusar/{consultaId}", null);
            response.EnsureSuccessStatusCode();
        }

    }
}
