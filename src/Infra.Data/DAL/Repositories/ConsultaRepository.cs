using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class ConsultaRepository : RepositoryBase<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(Contexto context) : base(context)
        {
        }


        public IList<Consulta> ObterConsultasNaoCanceladasDoDia(DateOnly dia, IList<Medico> medicosDiponiveis)
        {
            var medicoIds = medicosDiponiveis.Select(d => d.Id).ToList();

            return ObterQueryable()
                    .Where(x => x.Dia == dia && medicoIds.Contains(x.Medico.Id) && (x.Status == StatusConsulta.Criada || x.Status == StatusConsulta.Confirmada))
                    .ToList();
        }
    }
}
