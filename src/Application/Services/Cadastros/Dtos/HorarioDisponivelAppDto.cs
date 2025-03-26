using Domain.Entities.Cadastros;
using Domain.Enums;

namespace Application.Services.Cadastros.Dtos
{
    public class HorarioDisponivelAppDto
    {
        public int Id { get; set; }
        public DiaDaSemana DiaDaSemana { get; set; }
        public virtual Periodo Periodo { get; set; }
    }
}
