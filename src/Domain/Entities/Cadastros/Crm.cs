using Domain.Enums;

namespace Domain.Entities.Cadastros
{
    public class Crm
    {
        public string Numero { get; private set; }
        public UnidadeFederativa Uf { get; private set; }

        protected Crm() { }

        public Crm(string numero, UnidadeFederativa uf)
        {
            ValidarCrm(numero, uf);

            Numero = numero;
            Uf = uf;
        }

        private void ValidarCrm(string numero, UnidadeFederativa uf)
        {
            ValidarNumero(numero);
            ValidarUf(uf);
        }

        private void ValidarUf(UnidadeFederativa uf)
        {
            if (!Enum.IsDefined(typeof(UnidadeFederativa), uf)) throw new InvalidCastException($"A Unidade federativa '{uf}' não é válida.");
        }

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) throw new InvalidCastException($"CRM não pode ser nulo.");

            if (!numero.All(char.IsDigit) || numero.Length != 6) throw new InvalidCastException($"CRM '{numero}' não é válido.");
        }
    }
}
