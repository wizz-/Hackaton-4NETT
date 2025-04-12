namespace Application.Services.Cadastros.Dtos
{
    public class HorarioDisponivelDto
    {
        public int Id { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
        public virtual PeriodoDto? Periodo { get; set; }
    }
}
