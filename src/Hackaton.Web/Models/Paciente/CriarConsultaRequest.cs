namespace Hackaton.Web.Models.Paciente
{
    public class CriarConsultaRequest
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string Especialidade { get; set; }
        public DateTime DataHorario { get; set; }
    }
}
