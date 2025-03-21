using Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cadastros
{
    public class Medico
    {
        public int Id { get; private set; }
        public int Nome { get; private set; }
        public string Crm { get; private set; }
        public virtual IList<Especialidade> Especialidades { get; private set; }
        public virtual IList<HorarioDisponivel> HorariosDisponiveis { get; private set; }
        public virtual Usuario Usuario { get; private set; }
    }
}
