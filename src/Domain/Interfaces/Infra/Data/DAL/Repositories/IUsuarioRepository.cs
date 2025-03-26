using Domain.Entities.Login;

namespace Domain.Interfaces.Infra.Data.DAL.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario? ObterPorEmail(string email);
    }
}
