namespace Hackaton.Web.Models.Medico
{
    public class MeuCadastroMedicoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CrmNumero { get; set; }
        public string CrmUf { get; set; }
        public int EspecialidadeId { get; set; }
        public string Especialidade { get; set; }
        public decimal? ValorDaConsulta { get; set; }
        public List<MeuCadastroHorarioModel> Horarios { get; set; }
    }
}
