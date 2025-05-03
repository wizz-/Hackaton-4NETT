using Domain.Entities.Cadastros;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(Contexto context) : base(context)
        {
        }

        public Medico? ObterPorCrm(string crm, Domain.Enums.UnidadeFederativa uf)
        {
            return ObterQueryable()
                    .SingleOrDefault(x => x.Crm.Numero == crm && x.Crm.Uf == uf);
        }

        public IList<Medico> ObterPorDisponibilidadePorMedico(DayOfWeek dayOfWeek, int medicoId)
        {
            return ObterQueryable()
                .Where(x => x.HorariosDisponiveis.Any(x => x.DiaDaSemana == dayOfWeek && x.Id == medicoId))
                .ToList();
        }

        public IList<Medico> ObterPorDisponibilidadePorMeEspecialidade(DayOfWeek dayOfWeek, int especialidadeId)
        {
            return ObterQueryable()
                .Where(x => x.HorariosDisponiveis.Any(x => x.DiaDaSemana == dayOfWeek && x.Especialidade.Id == especialidadeId))
                .ToList();
        }

        public IList<Medico> ObterPorEspecialidade(int especialidadeId)
        {
            return ObterQueryable()
                .Where(x => x.Especialidade.Id == especialidadeId)
                .ToList();
        }
    }
}
