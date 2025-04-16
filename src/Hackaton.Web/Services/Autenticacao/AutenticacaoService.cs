using Hackaton.Web.Models.Autenticacao;
using Hackaton.Web.Models.Configuracao;
using Hackaton.Web.Services.Autenticacao.Interfaces;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Autenticacao
{
    public class AutenticacaoService(HttpClient http, CustomAuthStateProvider authProvider) : IAutenticacaoService
    {
        public async Task<bool> LoginAsync(AutenticacaoModel login)
        {
            HttpResponseMessage response;

            var (RequestEndPoint, RequestBody) = ObterDadosDoRequestPorPerfil(login.Perfil, login);
            response = await http.PostAsJsonAsync(RequestEndPoint, RequestBody);

            if (!response.IsSuccessStatusCode) return false;

            var resultado = await response.Content.ReadFromJsonAsync<TokenResponse>();

            if (resultado is null || string.IsNullOrWhiteSpace(resultado.Token)) return false;

            await authProvider.MarcarComoAutenticadoAsync(resultado.Token);
            return true;
        }

        private static (string RequestEndPoint, object RequestBody) ObterDadosDoRequestPorPerfil(
            PerfisDeUsuario perfilDoUsuario, 
            AutenticacaoModel model)
        {
            if (perfilDoUsuario == PerfisDeUsuario.Medico)
            {
                return ("login/medico", new AutenticacaoMedicoRequest()
                {
                    Crm = model.CRM!,
                    Uf = model.UF!,
                    Senha = model.Senha!
                });                
            }
            else if(perfilDoUsuario == PerfisDeUsuario.Paciente)
            {
                return ("login/paciente", new AutenticacaoPacienteRequest()
                {
                    CpfEmail = model.EmailOuCPF!,
                    Senha = model.Senha!
                });                
            }

            throw new ArgumentException("Perfil de usuário inválido.");
        }

        public async Task EncerrarSessaoAsync()
        {
            await authProvider.MarcarComoDeslogadoAsync();
        }
    }
}
