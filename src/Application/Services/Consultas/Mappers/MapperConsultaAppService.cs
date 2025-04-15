using Application.Services.Consultas.Dtos;
using Application.Services.Consultas.Mappers.Interfaces;
using Domain.Entities.Consultas;

namespace Application.Services.Consultas.Mappers
{
    public class MapperConsultaAppService : IMapperConsultaAppService
    {
        public IList<ConsultaDto> Map(IList<Consulta> consultas)
        {
            var destination = new List<ConsultaDto>();

            foreach (var item in consultas)
            {
                destination.Add(Map(item));
            }

            return destination;
        }

        private ConsultaDto Map(Consulta item)
        {
            var newItem = new ConsultaDto()
            {
                Id = item.Id,
                Dia = item.Dia,
                NomeDoPaciente = item.Paciente.Nome,
                Especialidade = item.Especialidade.Nome,
                HoraInicial = item.Horario.Inicio,
                HoraFinal = item.Horario.Fim,
                Status = item.Status.ToString(),
                MotivoDeCancelamento = item.MotivoDeCancelamento,
            };

            return newItem;
        }
    }
}
