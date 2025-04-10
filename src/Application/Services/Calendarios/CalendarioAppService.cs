﻿using Application.Services.Calendarios.Dtos;
using Application.Services.Calendarios.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Calendarios
{
    public class CalendarioAppService(IUnitOfWork unitOfWork) : ICalendarioAppService
    {
        public IList<MedicoAppDto> ObterCalendario(DateOnly dia, int especialidadeId)
        {
            var medicosDiponiveis = unitOfWork.MedicoRepository.ObterPorDisponibilidade(dia.DayOfWeek, especialidadeId);
            var consultasDoDia = unitOfWork.ConsultaRepository.ObterDoDia(dia, medicosDiponiveis);

            return CruzarDisponibilidadesComConsultas(dia, medicosDiponiveis, consultasDoDia);
        }

        private IList<MedicoAppDto> CruzarDisponibilidadesComConsultas(DateOnly dia, IList<Medico> medicosDiponiveis, IList<Consulta> consultasDoDia)
        {
            var retorno = new List<MedicoAppDto>();
            foreach (var item in medicosDiponiveis)
            {
                var medico = new MedicoAppDto()
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
