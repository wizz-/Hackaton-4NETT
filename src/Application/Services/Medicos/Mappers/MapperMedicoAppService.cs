using Application.Services.Medicos.Dtos;
using Application.Services.Medicos.Mappers.Interfaces;
using Domain.Entities.Cadastros;
using System.Globalization;

namespace Application.Services.Medicos.Mappers
{
    public class MapperMedicoAppService : IMapperMedicoAppService
    {
        public CadastroMedicoDto Map(Medico medico)
        {
            var newItem = new CadastroMedicoDto()
            {
                Id = medico.Id,
                Nome = medico.Nome,
                CrmNumero = medico.Crm.Numero,
                CrmUf = medico.Crm.Uf.ToString(),
                EspecialidadeId = medico.Especialidade.Id,
                Especialidade = medico.Especialidade.Nome,
                ValorDaConsulta = medico.ValorDaConsulta,
                Horarios = Map(medico.HorariosDisponiveis),
            };

            return newItem;
        }

        public IList<MedicoParaConsultaDto> Map(IList<Medico> medicos)
        {
            var destination = new List<MedicoParaConsultaDto>();

            foreach (var item in medicos)
            {
                destination.Add(MapMedicoParaConsulta(item));
            }

            return destination;
        }

        private MedicoParaConsultaDto MapMedicoParaConsulta(Medico item)
        {
            var newItem = new MedicoParaConsultaDto()
            {
                Id = item.Id,
                Nome = item.Nome,
                CrmNumero = item.Crm.Numero,
                CrmUf = item.Crm.Uf.ToString(),
                Especialidade = item.Especialidade.Nome,
            };

            return newItem;
        }

        private List<HorarioDisponivelDto> Map(IList<HorarioDisponivel>? horariosDisponiveis)
        {
            var destination = new List<HorarioDisponivelDto>();

            foreach (var item in horariosDisponiveis)
            {
                destination.Add(Map(item));
            }

            return destination;
        }

        private HorarioDisponivelDto Map(HorarioDisponivel item)
        {
            var newItem = new HorarioDisponivelDto()
            {
                Id = item.Id,
                DiaDaSemana = (int)item.DiaDaSemana,
                Inicio = item.Periodo.Inicio.ToString("HH:mm", CultureInfo.InvariantCulture),
                Fim = item.Periodo.Fim.ToString("HH:mm", CultureInfo.InvariantCulture),
            };

            return newItem;
        }
    }
}
