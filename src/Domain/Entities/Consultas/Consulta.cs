using Domain.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Consultas
{
    public class Consulta
    {
        public int Id { get; private set; }
        public virtual Paciente Paciente { get; private set; }
        public virtual Medico Medico { get; private set; }
        public virtual Especialidade Especialidade { get; private set; }
        public virtual Periodo Horario { get; private set; }
    }
}
