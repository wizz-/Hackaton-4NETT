using Application.Services.Cadastros.Dtos;

namespace Hackaton.Api.Controllers.Medicos.Dtos
{
    public class MedicoParaAtualizarDto
    {
        public int Id { get; set; }
        public decimal ValorDaConsulta { get; set; }
        public EspecialidadeDto Especialidade { get; set; }
        public IList<HorarioDisponivelDto> Horarios { get; set; }
    }
}
