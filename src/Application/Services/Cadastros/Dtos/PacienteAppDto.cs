using Application.Services.Logins.Dtos;

namespace Application.Services.Cadastros.Dtos
{
    public class PacienteAppDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public UsuarioAppDto Usuario { get; set; }
    }
}
