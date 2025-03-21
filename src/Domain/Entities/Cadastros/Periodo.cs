using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cadastros
{
    public class Periodo
    {
        public TimeOnly Inicio { get; private set; }
        public TimeOnly Fim { get; private set; }
    }
}
