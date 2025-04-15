namespace Application.Services.Consultas.Dtos
{
    public class ConsultaDto
    {
        public int Id { get; set; }
        public DateOnly Dia { get; set; }
        public string NomeDoPaciente { get; set; }
        public string Especialidade { get; set; }
        public TimeOnly HoraInicial { get; set; }
        public TimeOnly HoraFinal { get; set; }
        public string Status { get; set; }
        public string MotivoDeCancelamento { get; set; }
    }
}
