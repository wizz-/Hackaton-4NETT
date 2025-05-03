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

        public Especialidade? ObterPorNome(string especialidade)
        {
            return ObterQueryable()
                .SingleOrDefault(x => x.Nome == especialidade);
        }
    }
}
