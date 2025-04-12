using Hackaton.Web.Services.Ufs.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace Hackaton.Web.Services.Ufs
{
    public class UfService(HttpClient http) : IUfService
    {
        public async Task<List<string>> ObterUfAsync()
        {
            var ufs = await http.GetFromJsonAsync<List<string>>("ufs");
            return ufs ?? [];
        }
    }
}
