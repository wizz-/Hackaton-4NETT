namespace Hackaton.Web.Models.Medico
{
    public class ConsultaMedicoModel
    {
        public int Id { get; set; }
        public DateOnly Dia { get; set; }
        public string NomeDoMedico { get; set; } = string.Empty;
        public string NomeDoPaciente { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string HoraInicial { get; set; } = string.Empty;
        public string HoraFinal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? MotivoDeCancelamento { get; set; }

        public string DataFormatada => Dia.ToString("dd/MM/yyyy");
        public string Horario => $"{HoraInicial[..5]} às {HoraFinal[..5]}";
    }
}
