using Domain.Entities.Cadastros;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class PacienteRepository : RepositoryBase<Paciente>, IPacienteRepository
    {
        public PacienteRepository(Contexto context) : base(context)
        {
        }

        public Paciente? ObterPorCpf(string cpf)
        {
            return ObterQueryable()
                    .SingleOrDefault(x => x.Cpf == cpf);
        }

        public Paciente? ObterPorEmail(string email)
        {
            return ObterQueryable()
                    .SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
