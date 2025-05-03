using Application.Services.Agendas.Dtos;
using Application.Services.Agendas.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Exceptions;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Agendas
{
    public class AgendaAppService(IUnitOfWork unitOfWork) : IAgendaAppService
    {
        public IList<MedicoDisponivelDto> ObterAgendaPorEspecialidade(DateOnly dia, int especialidadeId)
        {
            var medicosDiponiveis = unitOfWork.MedicoRepository.ObterPorDisponibilidadePorMeEspecialidade(dia.DayOfWeek, especialidadeId);
            if (medicosDiponiveis == null || medicosDiponiveis.Count == 0)
                throw new NotFoundException("Médico disponível");

            var consultasDoDia = unitOfWork.ConsultaRepository.ObterConsultasNaoCanceladasDoDia(dia, medicosDiponiveis);
            if (consultasDoDia == null || consultasDoDia.Count == 0)
                throw new NotFoundException("Consultas para o dia");

            return CruzarDisponibilidadesComConsultas(dia, medicosDiponiveis, consultasDoDia);
        }

        public IList<AgendaAppDto> ObterAgendaPorMedico(DateOnly diaInicial, int quantidadeDeDias, int medicoId)
        {
            var dataFinal = diaInicial.AddDays(quantidadeDeDias);
            var medico = unitOfWork.MedicoRepository.ObterPorId(medicoId);
            if (medico == null) throw new NotFoundException($"Médico com id '{medicoId}' não existe.");

            var consultasDosDias = unitOfWork.ConsultaRepository.ObterConsultasNaoCanceladasNoPeriodo(diaInicial, dataFinal, medico);


            return CruzarDisponibilidadesComConsultas(diaInicial, dataFinal, medico, consultasDosDias);
        }

        private IList<AgendaAppDto> CruzarDisponibilidadesComConsultas(DateOnly diaInicial, DateOnly dataFinal, Medico medico, IList<Consulta> consultasDosDias)
        {
            var retorno = new List<AgendaAppDto>();
            for (var dataIterada = diaInicial; dataIterada < dataFinal; dataIterada = dataIterada.AddDays(1))
            {
                var horariosOcupados = consultasDosDias.Where(x => x.Dia == dataIterada).Select(x => x.Horario).ToList();

                var agenda = new AgendaAppDto()
                {
                    Dia = dataIterada,
                    Horarios = medico.ObterHorasDiponiveis(dataIterada.DayOfWeek, horariosOcupados)
                };

                retorno.Add(agenda);
            }

            return retorno;
        }

        private IList<MedicoDisponivelDto> CruzarDisponibilidadesComConsultas(DateOnly dia, IList<Medico> medicosDiponiveis, IList<Consulta> consultasDoDia)
        {
            var retorno = new List<MedicoDisponivelDto>();
            foreach (var item in medicosDiponiveis)
            {
                var medico = new MedicoDisponivelDto()
                {
                    Id = item.Id,
                    CrmNumero = item.Crm!.Numero,
                    CrmUf = item.Crm!.Uf,
                    Nome = item.Nome!,
                    HorariosDisponiveis = item.ObterHorasDiponiveis(dia.DayOfWeek, consultasDoDia.Where(x => x.Medico.Id == item.Id).Select(x => x.Horario).ToList()),
                };

                retorno.Add(medico);
            }

            return retorno;
        }
    }
}
