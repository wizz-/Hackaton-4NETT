namespace Application.Services.Cadastros.Dtos
{
    public class HorarioDisponivelAppDto
    {
        public int Id { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
        public virtual PeriodoAppDto? Periodo { get; set; }
    }
}
