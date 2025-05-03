namespace Hackaton.Web.Models.Medico
{
    public class ConsultaMedicoModel
    {
        public int Id { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Horario { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
