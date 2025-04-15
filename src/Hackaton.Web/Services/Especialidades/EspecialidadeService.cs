using Hackaton.Web.Models.Especialidade;
using Hackaton.Web.Services.Especialidades.Interfaces;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Especialidades
{
    public class EspecialidadeService(HttpClient http) : IEspecialidadeService
    {
        public async Task<List<EspecialidadeModel>> ObterEspecialidadesAsync()
        {
            var especialidades = await http.GetFromJsonAsync<List<EspecialidadeModel>>("especialidades");
            return especialidades ?? [];
        }
    }
}
