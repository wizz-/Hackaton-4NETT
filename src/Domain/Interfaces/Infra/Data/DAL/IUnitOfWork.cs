using Domain.Interfaces.Infra.Data.DAL.Repositories;

namespace Domain.Interfaces.Infra.Data.DAL
{
    public interface IUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }
        IPacienteRepository PacienteRepository { get; }
        IEspecialidadeRepository EspecialidadeRepository { get; }
        IMedicoRepository MedicoRepository { get; }

        void SaveChanges();
    }
}
