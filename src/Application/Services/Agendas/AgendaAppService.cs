using Application.Services.Agendas.Dtos;
using Application.Services.Agendas.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Agendas
{
    public class AgendaAppService(IUnitOfWork unitOfWork) : IAgendaAppService
    {
        public IList<MedicoDisponivelDto> ObterAgenda(DateOnly dia, int especialidadeId)
        {
            var medicosDiponiveis = unitOfWork.MedicoRepository.ObterPorDisponibilidade(dia.DayOfWeek, especialidadeId);
            var consultasDoDia = unitOfWork.ConsultaRepository.ObterConsultasNaoCanceladasDoDia(dia, medicosDiponiveis);

            return CruzarDisponibilidadesComConsultas(dia, medicosDiponiveis, consultasDoDia);
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
