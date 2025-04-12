using Application.Services.Especialidades.Dtos;
using Application.Services.Especialidades.Interfaces;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Especialidades
{
    public class EspecialidadesAppService(IUnitOfWork unitOfWork) : IEspecialidadesAppService
    {
        public IList<EspecialidadeDto> ObterEspecialidades()
        {
            var especialidades = unitOfWork.EspecialidadeRepository.ObterTodos();

            return especialidades
                .Select(x => new EspecialidadeDto { Id = x.Id, Nome = x.Nome })
                .ToList();
        }
    }
}
