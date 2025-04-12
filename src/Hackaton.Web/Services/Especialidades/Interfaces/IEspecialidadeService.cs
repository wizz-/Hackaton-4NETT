using Hackaton.Web.Models;

namespace Hackaton.Web.Services.Especialidades.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<List<EspecialidadeModel>> ObterEspecialidadesAsync();
    }
}
