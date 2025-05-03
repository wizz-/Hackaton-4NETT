namespace Hackaton.Web.Models.Paciente
{
    public class AgendaHorarioModel
    {
        public DateOnly Dia { get; set; }
        public IList<TimeOnly>? Horarios { get; set; }
    }
}
