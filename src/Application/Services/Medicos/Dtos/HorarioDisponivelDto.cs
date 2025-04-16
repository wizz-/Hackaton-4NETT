namespace Application.Services.Medicos.Dtos
{
    public class HorarioDisponivelDto
    {
        public int Id { get; set; }
        public int DiaDaSemana { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
    }
}
