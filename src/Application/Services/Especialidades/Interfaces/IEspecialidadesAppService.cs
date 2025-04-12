using Application.Services.Especialidades.Dtos;

namespace Application.Services.Especialidades.Interfaces
{
    public interface IEspecialidadesAppService
    {
        IList<EspecialidadeDto> ObterEspecialidades();
    }
}
