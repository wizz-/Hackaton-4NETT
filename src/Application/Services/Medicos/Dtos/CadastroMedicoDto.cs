namespace Application.Services.Medicos.Dtos
{
    public class CadastroMedicoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CrmNumero { get; set; }
        public string CrmUf { get; set; }
        public int EspecialidadeId { get; set; }
        public string Especialidade { get; set; }
        public decimal? ValorDaConsulta { get; set; }
        public List<HorarioDisponivelDto> Horarios { get; set; }
    }
}
