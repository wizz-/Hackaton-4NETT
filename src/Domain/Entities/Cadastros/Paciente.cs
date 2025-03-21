using Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cadastros
{
    public class Paciente
    {
        public int Id { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public virtual Usuario Usuario { get; private set; }
    }
}
