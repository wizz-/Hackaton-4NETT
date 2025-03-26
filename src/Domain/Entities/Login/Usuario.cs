using Domain.Enums;
using Konscious.Security.Cryptography;
using System.Security;
using System.Security.Cryptography;

namespace Domain.Entities.Login
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Email { get; private set; } //Para possível lógica futura de recuperação de senha
        public string SenhaHash { get; private set; }
        public string Salt { get; private set; }
        public TipoDeUsuario Tipo { get; set; }

        protected Usuario() { } // Para ORM

        public Usuario(string email, SecureString senha, TipoDeUsuario tipo)
        {
            Email = email;
            Tipo = tipo;

            Salt = GerarSalt();
            SenhaHash = GerarHash(senha, Salt);
        }

        public bool ValidarSenha(SecureString senha)
        {
            var hashTentativa = GerarHash(senha, Salt);
            return SenhaHash == hashTentativa;
        }

        private string GerarSalt()
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);

            return Convert.ToBase64String(saltBytes);
        }

        private string GerarHash(SecureString senha, string salt)
        {
            var senhaBytes = SecureStringParaBytes(senha);

            using (var argon2 = new Argon2id(senhaBytes))
            {
                argon2.Salt = Convert.FromBase64String(salt);
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 128000;
                argon2.Iterations = 6;

                return Convert.ToBase64String(argon2.GetBytes(32));
            }
        }

        private byte[] SecureStringParaBytes(SecureString secureString)
        {
            if (secureString == null) throw new ArgumentNullException(nameof(secureString));

            var ptr = IntPtr.Zero;
            try
            {
                ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
                var length = System.Runtime.InteropServices.Marshal.ReadInt32(ptr, -4);
                var bytes = new byte[length];

                for (int i = 0; i < length; i++)
                {
                    bytes[i] = System.Runtime.InteropServices.Marshal.ReadByte(ptr, i);
                }

                return bytes;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
