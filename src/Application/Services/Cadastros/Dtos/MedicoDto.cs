using Application.Services.Logins.Dtos;
using Domain.Enums;

namespace Application.Services.Cadastros.Dtos
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CrmNumero { get; set; }
        public UnidadeFederativa CrmUf { get; set; }
        public virtual EspecialidadeDto Especialidade { get; set; }
        public virtual UsuarioAppDto Usuario { get; set; }
    }
}
