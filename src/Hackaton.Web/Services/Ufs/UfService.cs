using Hackaton.Web.Services.Ufs.Interfaces;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Ufs
{
    public class UfService(HttpClient Http) : IUfService
    {
        public async Task<List<string>> ObterUfAsync()
        {
            var estados = await Http.GetFromJsonAsync<List<string>>("/ufs");
            return estados ?? [];
        }
    }
}
