namespace Hackaton.Web.Models.Medico
{
    public class MedicoMeuCadastroRequest
    {        
        public int Id { get; set; }
        public decimal ValorDaConsulta { get; set; }
        public object Especialidade { get; set; }
        public IList<MeuCadastroHorarioModel> Horarios { get; set; }        
    }
}
