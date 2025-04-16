namespace Application.Services.Cadastros.Dtos
{
    public class HorarioDisponivelDto
    {
        public int Id { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
    }
}
