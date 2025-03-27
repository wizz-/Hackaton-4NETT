using Application.Services.Logins.Dtos;
using Domain.Enums;

namespace Application.Services.Cadastros.Dtos
{
    public class MedicoAppDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CrmNumero { get; set; }
        public UnidadeFederativa CrmUf { get; set; }
        public int TempoDeConsulta { get; set; }
        public virtual IList<EspecialidadeAppDto> Especialidades { get; set; }
        public virtual IList<HorarioDisponivelAppDto> HorariosDisponiveis { get; set; }
        public virtual UsuarioAppDto Usuario { get; set; }
    }
}
