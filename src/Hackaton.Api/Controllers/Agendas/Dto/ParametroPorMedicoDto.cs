
namespace Hackaton.Api.Controllers.Agendas.Dto
{
    public class ParametroPorMedicoDto
    {
        public DateOnly DataInicial { get; set; }
        public int QuantidadeDeDias { get; set; }
        public int MedicoId { get; set; }
    }
}
