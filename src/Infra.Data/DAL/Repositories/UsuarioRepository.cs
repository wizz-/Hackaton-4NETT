using Domain.Entities.Login;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(Contexto context) : base(context)
        {
        }

        public Usuario? ObterPorEmail(string email)
        {
            return ObterQueryable()
                    .FirstOrDefault(x => x.Email == email);
        }
    }
}
