namespace Hackaton.Web.Exceptions
{
    public class ApiException : ApplicationException
    {
        public string? Detalhes { get; }

        public ApiException(string mensagem, string? detalhes = null)
            : base(mensagem)
        {
            Detalhes = detalhes;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Detalhes)
                ? Message
                : $"{Message}\nDetalhes: {Detalhes}";
        }
    }
}
