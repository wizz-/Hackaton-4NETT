using System.Text.Json;

namespace Hackaton.Web.Infra
{
    public static class JsonOptionsDefaults
    {
        public static readonly JsonSerializerOptions Web = new(JsonSerializerDefaults.Web);
    }
}
