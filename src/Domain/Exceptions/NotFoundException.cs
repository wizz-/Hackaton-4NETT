using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        const string message = "Não foi encontrado: {0}";
        public NotFoundException(string item) : base(string.Format(message, item))
        {

        }
    }
}
