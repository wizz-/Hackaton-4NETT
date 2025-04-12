using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hackaton.Api.Converters
{
    public class SecureStringJsonConverter : JsonConverter<SecureString>
    {
        public override SecureString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null) return null;

            return ConvertToSecureString(value);
        }

        public override void Write(Utf8JsonWriter writer, SecureString value, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Não é possível converter uma SecureString em um JSON pois seria um risco de segurança.");
        }

        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
            {
                return null;
            }

            SecureString secureString = new SecureString();
            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();
            return secureString;
        }
    }
}
