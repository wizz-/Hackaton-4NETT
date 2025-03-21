using Domain.Enums;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public Usuario(string email, string senha, TipoDeUsuario tipo)
        {
            Email = email;
            Tipo = tipo;

            Salt = GerarSalt();
            SenhaHash = GerarHash(senha, Salt);
        }

        public bool ValidarSenha(string senha)
        {
            var hashTentativa = GerarHash(senha, Salt);
            return SenhaHash == hashTentativa;
        }

        private string GerarSalt()
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);

            return Convert.ToBase64String(saltBytes);
        }

        private string GerarHash(string senha, string salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha)))
            {
                argon2.Salt = Convert.FromBase64String(salt);
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 128000;
                argon2.Iterations = 6;

                return Convert.ToBase64String(argon2.GetBytes(32));
            }
        }
    }
}
