using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cadastros
{
    public class HorarioDisponivel
    {
        public int Id { get; private set; }
        public DiaDaSemana DiaDaSemana { get; private set; }
        public virtual Periodo Periodo { get; private set; }
    }
}
