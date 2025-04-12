using Domain.Entities.Cadastros;

namespace Domain.Interfaces.Infra.Data.DAL.Repositories
{
    public interface IPacienteRepository : IRepositoryBase<Paciente>
    {
        Paciente? ObterPorCpf(string cpf);
        Paciente? ObterPorEmail(string email);
    }
}
