using Domain.Enums;

namespace Application.Services.Agendas.Dtos
{
    public class MedicoDisponivelDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CrmNumero { get; set; } = string.Empty;
        public UnidadeFederativa CrmUf { get; set; }
        public IList<TimeOnly>? HorariosDisponiveis { get; set; }
    }
}
