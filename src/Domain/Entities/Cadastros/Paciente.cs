using Domain.Entities.Login;

namespace Domain.Entities.Cadastros
{
    public class Paciente
    {
        public int Id { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        protected Paciente()
        {
        }

        public Paciente(string cpf, string email, string nome, Usuario usuario)
        {
            ValidarCampos(cpf, email, nome, usuario);

            Cpf = LimparFormato(cpf);
            Email = email;
            Nome = nome;
            Usuario = usuario;
        }

        private void ValidarCampos(string cpf, string email, string nome, Usuario usuario)
        {
            ValidarCpf(cpf);
            ValidarEmail(email);
            ValidarNome(nome);
            ValidarUsuario(usuario);
        }

        private void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null) throw new InvalidOperationException("Usuário Inválido.");
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new InvalidOperationException("Nome Inválido.");
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new InvalidOperationException("E-mail Inválido.");

            if (!email.Contains("@") && email.IndexOf(".", email.IndexOf("@")) > email.IndexOf("@")) throw new InvalidOperationException("E-mail Inválido.");
        }

        public static void ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) throw new InvalidOperationException("CPF Inválido.");

            cpf = LimparFormato(cpf);

            if (cpf.Length != 11 || cpf.Distinct().Count() == 1) throw new InvalidOperationException("CPF Inválido.");

            var multiplicador1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfSemDigito = cpf.Substring(0, 9);
            var soma = cpfSemDigito.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            cpfSemDigito += digito1;
            soma = cpfSemDigito.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            if (!cpf.EndsWith(digito1.ToString() + digito2.ToString())) throw new InvalidOperationException("CPF Inválido.");
        }

        private static string LimparFormato(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }


    }
}
