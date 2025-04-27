namespace Hackaton.Web.Models.Paciente
{
    public class AgendaRequestModel
    {
        public DateOnly DataInicial { get; set; }
        public int QuantidadeDeDias { get; set; }
        public int MedicoId { get; set; }
    }
}
