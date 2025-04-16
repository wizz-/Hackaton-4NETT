using Hackaton.Web.Exceptions;
using Hackaton.Web.Infra;
using Hackaton.Web.Models.Erros;
using Hackaton.Web.Models.Especialidade;
using Hackaton.Web.Models.Medico;
using Hackaton.Web.Models.Usuario;
using Hackaton.Web.Services.Medicos.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Hackaton.Web.Services.Medicos
{
    public class MedicoService(HttpClient http) : IMedicoService
    {

        public async Task<MeuCadastroMedicoModel> ObterMedico(int id)
        {
            //try
            //{
            var medico = await http.GetFromJsonAsync<MeuCadastroMedicoModel>($"medicos/{id}");

            //var teste = medico.Content.ReadAsStringAsync();

            //throw new NotImplementedException();
            return medico!;

            //}
            //catch (Exception ex)
            //{
            //    var teste = ex;
            //}

            //throw new NotImplementedException();

            return null;
        }

        public async Task CadastrarMedicoAsync(PrimeiroAcessoMedicoModel medico)
        {
            ArgumentNullException.ThrowIfNull(medico);
            var parametroDaRequest = PreencherMedicoRequest(medico);

            var response = await http.PostAsJsonAsync("medicos", parametroDaRequest);

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

        private MedicoCadastroRequest PreencherMedicoRequest(PrimeiroAcessoMedicoModel model)
        {
            return new MedicoCadastroRequest
            {
                Nome = model.NomeCompleto,
                CrmNumero = model.CRM,
                CrmUf = model.UF,
                Especialidade = new EspecialidadeModel
                {
                    Id = model.EspecialidadeId!.Value,
                    Nome = model.EspecialidadeNome
                },
                Usuario = new UsuarioRequest
                {
                    Email = model.Email,
                    Senha = model.Senha
                }
            };
        }
    }
}
