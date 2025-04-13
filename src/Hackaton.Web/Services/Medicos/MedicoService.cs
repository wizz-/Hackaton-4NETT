using Hackaton.Web.Models.Medico;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Medicos
{
    public class MedicoService(HttpClient http) : IMedicoService
    {
        public async Task CadastrarMedicoAsync(MedicoCadastroRequest medico)
        {
            var response = await http.PostAsJsonAsync("medicos", medico);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
