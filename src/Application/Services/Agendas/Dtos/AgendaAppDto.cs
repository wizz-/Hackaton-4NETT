namespace Application.Services.Agendas.Dtos
{
    public class AgendaAppDto
    {
        public DateOnly Dia { get; set; }
        public IList<TimeOnly>? Horarios { get; set; }
    }
}
