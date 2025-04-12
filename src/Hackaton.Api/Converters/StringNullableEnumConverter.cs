using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hackaton.Api.Converters
{
    public class JsonNullableStringEnumConverter(
        JsonNamingPolicy? namingPolicy = null,
        bool allowIntegerValues = true) : JsonConverterFactory
    {
        readonly JsonStringEnumConverter stringEnumConverter = new(namingPolicy, allowIntegerValues);

        public override bool CanConvert(Type typeToConvert)
            => Nullable.GetUnderlyingType(typeToConvert)?.IsEnum == true;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var tipoParaConverter = Nullable.GetUnderlyingType(typeToConvert)!;
            var tipoGenerico = typeof(ValueConverter<>).MakeGenericType(tipoParaConverter);
            var parametrosDaConversao = stringEnumConverter.CreateConverter(tipoParaConverter, options);

            return (JsonConverter)Activator.CreateInstance(tipoGenerico, parametrosDaConversao)!;
        }

        class ValueConverter<T> : JsonConverter<T?>
            where T : struct, Enum
        {
            readonly JsonConverter<T> converter;

            public ValueConverter(JsonConverter<T> converter)
            {
                this.converter = converter;
            }

            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    reader.Read();
                    return null;
                }
                return converter.Read(ref reader, typeof(T), options);
            }

            public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
            {
                if (value == null)
                    writer.WriteNullValue();
                else
                    converter.Write(writer, value.Value, options);
            }
        }
    }
}
