using Domain.Entities.Cadastros;

namespace Domain.Interfaces.Infra.Data.DAL.Repositories
{
    public interface IMedicoRepository : IRepositoryBase<Medico>
    {
        Medico? ObterPorCrm(string crm, Enums.UnidadeFederativa uf);
        IList<Medico> ObterPorDisponibilidadePorMedico(DayOfWeek dayOfWeek, int medicoId);
        IList<Medico> ObterPorDisponibilidadePorMeEspecialidade(DayOfWeek dayOfWeek, int especialidadeId);
        IList<Medico> ObterPorEspecialidade(int especialidadeId);
    }
}
