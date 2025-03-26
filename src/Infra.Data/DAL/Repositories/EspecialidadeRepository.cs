using Domain.Entities.Cadastros;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class EspecialidadeRepository : RepositoryBase<Especialidade>, IEspecialidadeRepository
    {
        public EspecialidadeRepository(Contexto context) : base(context)
        {
        }
    }
}
