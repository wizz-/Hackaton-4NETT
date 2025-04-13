using Hackaton.Web.Models.Especialidade;

namespace Hackaton.Web.Services.Especialidades.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<List<EspecialidadeModel>> ObterEspecialidadesAsync();
    }
}
