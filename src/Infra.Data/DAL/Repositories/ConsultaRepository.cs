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

        public IList<Consulta> ObterConsultasFuturasPorMedico(int medicoId)
        {
            var diaAtual = DateTime.Now;

            var dia = DateOnly.FromDateTime(diaAtual);
            var hora = TimeOnly.FromDateTime(diaAtual);

            return ObterQueryable()
                .Where(x => x.Medico.Id == medicoId && x.Dia == dia && x.Horario.Inicio >= hora)
                .ToList();
        }

        public IList<Consulta> ObterConsultasFuturasPorPaciente(int pacienteId)
        {
            var diaAtual = DateTime.Now;

            var dia = DateOnly.FromDateTime(diaAtual);
            var hora = TimeOnly.FromDateTime(diaAtual);

            return ObterQueryable()
                .Where(x => x.Paciente.Id == pacienteId && x.Dia == dia && x.Horario.Inicio >= hora)
                .ToList();
        }

        public IList<Consulta> ObterConsultasNaoCanceladasDoDia(DateOnly dia, IList<Medico> medicosDiponiveis)
        {
            var medicoIds = medicosDiponiveis.Select(d => d.Id).ToList();

            return ObterQueryable()
                    .Where(x => x.Dia == dia && medicoIds.Contains(x.Medico.Id) && (x.Status == StatusConsulta.Pendente || x.Status == StatusConsulta.Confirmada))
                    .ToList();
        }

        public IList<Consulta> ObterConsultasNaoCanceladasNoPeriodo(DateOnly dataInicial, DateOnly dataFinal, Medico medico)
        {
            return ObterQueryable()
                    .Where(x =>
                            x.Dia >= dataInicial &&
                            x.Dia <= dataFinal &&
                            x.Medico.Id == medico.Id &&
                            (x.Status == StatusConsulta.Pendente || x.Status == StatusConsulta.Confirmada)
                        )
                    .ToList();
        }
    }
}
